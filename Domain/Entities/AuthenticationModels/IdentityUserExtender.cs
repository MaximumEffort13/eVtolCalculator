using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.AuthenticationModels;

public class IdentityUserExtender : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpirationDate { get; set; }
}
