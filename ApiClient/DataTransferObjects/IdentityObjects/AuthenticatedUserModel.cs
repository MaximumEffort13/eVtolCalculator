namespace ApiClient.DataTransferObjects.IdentityObjects;

public class AuthenticatedUserModel
{
    public string AccessToken { get; }
    public string UserName { get; }
    public string RefreshToken { get; }
    public int AccessTokenExpInMin { get; }
    public int RefreshTokenExpInDays { get; }
}
