namespace ApiClient.DataTransferObjects.IdentityObjects;

public class AuthenticatedUserModel
{
    public string AccessToken { get; set; }
    public string UserName { get; set; }
    public string RefreshToken { get; set; }
    public int AccessTokenExpInMin { get; set; }
    public int RefreshTokenExpInDays { get; set; }
}
