namespace ApiClient.DataTransferObjects;

public class AuthenticatedUserModel
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public double ExpiresIn { get; set; }
}
