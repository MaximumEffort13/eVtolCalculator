using Domain.Entities.AuthenticationModels;
using eVtolCalculatorApi.Models;
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
    private readonly UserManager<IdentityUserExtender> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public TokenController(ISender sender, UserManager<IdentityUserExtender> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _sender = sender;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public sealed record LoginDetails(string Username, string Password);

    [Route("Authenticate")]
    [HttpPost]
    public async Task<IActionResult> Create(LoginDetails loginUser)
    {
        if(await IsValidUsernameAndPassword(loginUser.Username, loginUser.Password) == false)
        {
            return BadRequest();
        }

        var user = await _userManager.FindByNameAsync(loginUser.Username);

        var newTokens = await GenerateToken(loginUser.Username);

        user.RefreshToken = newTokens.RefreshToken;
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(newTokens.RefreshTokenExpInDays);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded == false)
        {
            return BadRequest();
        }

        return Ok(newTokens);
    }

    public sealed record RefreshModel(string ExpiredAccessToken, string RefreshToken);

    [Route("Refresh")]
    [HttpPost]
    public async Task<IActionResult> Refresh(RefreshModel refreshModel)
    {
        if (refreshModel == null)
        {
            return BadRequest();
        }

        var user = await ExtractUserDetails(refreshModel);

        if (user is null
        || refreshModel.RefreshToken != user.RefreshToken
        || user.RefreshTokenExpirationDate.CompareTo(DateTime.UtcNow.AddDays(_config.GetValue<int>("JwtSettings:RefreshTokenExp"))) > 0)
        {
            return BadRequest();
        }

        var newAccessTokens = await GenerateToken(user.Email!);

        if (newAccessTokens is null)
        {
            return BadRequest();
        }

        user.RefreshToken = newAccessTokens.RefreshToken;
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(newAccessTokens.RefreshTokenExpInDays);

        await _userManager.UpdateAsync(user);

        return Ok(newAccessTokens);
    }

    private async Task<bool> IsValidUsernameAndPassword(string username, string password)
    {
        var user = await _userManager.FindByEmailAsync(username);
        return await _userManager.CheckPasswordAsync(user, password);
    }

    private async Task<TokenDto> GenerateToken(string username)
    {
        var user = await _userManager.FindByEmailAsync(username);
        var roles = await _userManager.GetRolesAsync(user);

        var validAudience = _config.GetValue<string>("JwtSettings:ValidAudience");
        var validIssuer = _config.GetValue<string>("JwtSettings:ValidIssuer");
        var accessTokenExpInMin = _config.GetValue<int>("JwtSettings:AccessTokenExpInMin");
        var refreshTokenValidityInDays = _config.GetValue<int>("JwtSettings:RefreshTokenExpInDays");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(accessTokenExpInMin)).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, validIssuer!),
            new Claim(JwtRegisteredClaimNames.Aud, validAudience!),
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

        var refreshToken = GenerateRefreshToken();

        var output = new TokenDto(
                            new JwtSecurityTokenHandler().WriteToken(token),
                            username,
                            new JwtSecurityTokenHandler().WriteToken(refreshToken),
                            accessTokenExpInMin,
                            refreshTokenValidityInDays);

        return output;
    }

    private JwtSecurityToken GenerateRefreshToken()
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config.GetValue<string>("Secrets:SecurityKey")!));
        var tokenValidityInDays = _config.GetValue<int>("JwtSettings:RefreshTokenExpInDays");
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(tokenValidityInDays)).ToUnixTimeSeconds().ToString()),
        };

        var refreshToken = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

        return refreshToken;
    }

    private async Task<IdentityUserExtender?> ExtractUserDetails(RefreshModel refreshModel)
    {
        string? accessToken = refreshModel.ExpiredAccessToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);

        if (principal is null)
        {
            return null;
        }

        string userName = principal.Identity?.Name ?? string.Empty;
        IdentityUserExtender? user = await _userManager.FindByNameAsync(userName);

        return user;
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Secrets:SecurityKey"]!)),
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}
