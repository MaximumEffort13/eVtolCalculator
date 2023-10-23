using Application.Commands.Person;
using Application.Queries.Person;
using eVtolCalculatorApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserController> _logger;

    public UserController(ISender sender,
                          UserManager<IdentityUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          ILogger<UserController> logger)
    {
        _sender = sender;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken) 
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return NotFound();
        }

        var query = new GetPersonByUserIdQuery(Guid.Parse(userId));
        var response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response) : BadRequest(response.Errors);
    }

    //Record is a once of read only data. Similar to class with properties and constructor that initializes the properties |property { get; init; }|
    public record UserRegistrationModel(string FirstName,
                                        string LastName,
                                        string EmailAddress,
                                        string PhoneNumber,
                                        string Password,
                                        string StreetName,
                                        string Suburb,
                                        string City,
                                        string Province,
                                        string Country,
                                        string PostalCode);

    [AllowAnonymous]
    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationModel user, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest();
        }

        var existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);
        if (existingUser != null)
        {
            return BadRequest();
        }

        IdentityUser newUser = new()
        {
            Email = user.EmailAddress,
            EmailConfirmed = true,
            UserName = user.EmailAddress
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded == false)
        {
            return BadRequest();
        }

        existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);

        if (existingUser == null)
        {
            return BadRequest();
        }

        var userIdGuid = Guid.Parse(existingUser.Id!);

        CreatePersonCommand person = new
        (
            userIdGuid,
            user.FirstName,
            user.LastName,
            user.EmailAddress,
            user.PhoneNumber,
            user.StreetName,
            user.Suburb,
            user.City,
            user.Province,
            user.PostalCode
        );

        var personResult = await _sender.Send(person, cancellationToken);

        if (personResult.IsSuccess == false)
        {
            return BadRequest();
        }

        await AddEveryoneRole(existingUser.Id);

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [Route("Admin/GetAllUsers")]
    [HttpGet]
    public async Task<List<ApplicationUserModel>> GetAllUsers()
    {

        List<ApplicationUserModel> output = new List<ApplicationUserModel>();

        var users = _userManager.Users.ToList();

        foreach (var user in users)
        {
            ApplicationUserModel u = new ApplicationUserModel
            {
                Id = user.Id,
                Email = user.Email,
            };

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            u.Roles = new Dictionary<string, string>();
            
            foreach(var role in roles)
            {
                if (userRoles.Contains(role.Name))
                {
                    u.Roles.Add(role.Id, role.Name);
                }
            }

            output.Add(u);
        }

        return output;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("Admin/GetAllRoles")]
    public Dictionary<string, string> GetAllRoles()
    {
        var roles = _roleManager.Roles.ToDictionary(x => x.Id, x => x.Name);

        return roles;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("Admin/AddRole")]
    public async Task AddRole(UserRolePairModel pairing)
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(pairing.UserId);

        _logger.LogInformation("Admin {Admin} added user {User} to role {Role}",
            loggedInUserId,
            user.Id,
            pairing.RoleName);

        await _userManager.AddToRoleAsync(user, pairing.RoleName);
    }

    [Authorize]
    [HttpPost]
    public async Task AddEveryoneRole(string newUserId)
    {
        var user = await _userManager.FindByIdAsync(newUserId);
        await _userManager.AddToRoleAsync(user, "Everyone");
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("Admin/RemoveRole")]
    public async Task RemoveRole(UserRolePairModel pairing)
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(pairing.UserId);

        _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
            loggedInUserId,
            user.Id,
            pairing.RoleName);

        await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);
    }
}
