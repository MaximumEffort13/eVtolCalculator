using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.IdentityObjects;

public class AuthenticationUserModel
{
    [Required(ErrorMessage = "Email Address is required.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    public string TwoFactorCode { get; set; } = string.Empty;

    public string TwoFactorRecoveryCode { get; set; } = string.Empty;

}
