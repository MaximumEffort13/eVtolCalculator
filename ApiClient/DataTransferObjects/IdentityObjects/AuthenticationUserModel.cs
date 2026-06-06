using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.IdentityObjects;

public class AuthenticationUserModel
{
    [Required(ErrorMessage = "Email Address is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

}
