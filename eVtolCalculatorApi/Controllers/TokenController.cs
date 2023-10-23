using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : Controller
{
    private readonly ISender _sender;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public TokenController(ISender sender, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _sender = sender;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public sealed record LoginDetails(string Username, string Password);

    [Route("/token")]
    [HttpPost]
    public async Task<IActionResult> Create(LoginDetails loginUser)
    {
        if(await IsValidUsernameAndPassword(loginUser.Username, loginUser.Password) == false)
        {
            return BadRequest();
        }

        return new ObjectResult(await GenerateToken(loginUser.Username));
    }

    private async Task<bool> IsValidUsernameAndPassword(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    private async Task<dynamic> GenerateToken(string username)
    {

        var user = await _userManager.FindByEmailAsync(username);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:7197"),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        string key = _config.GetValue<string>("Secrets:SecurityKey");

        var token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256)),
            new JwtPayload(claims));

        var output = new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = username
        };

        return output;
    }
}
