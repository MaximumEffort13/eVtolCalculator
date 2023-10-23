using eVtolCalculatorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eVtolCalculatorApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Admin()
    {
        string[] roles = { "Admin", "Management", "Everyone" };

        foreach (var role in roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (roleExists == false)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var user = await _userManager.FindByEmailAsync("jacobus@mecalc.com");

        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "Management");
            await _userManager.AddToRoleAsync(user, "Everyone");
        }

        return Ok();
    }
}
