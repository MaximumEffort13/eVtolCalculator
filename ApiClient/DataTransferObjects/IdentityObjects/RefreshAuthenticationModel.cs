namespace ApiClient.DataTransferObjects.IdentityObjects;

public sealed class RefreshAuthenticationModel
{
    public string ExpiredAccessToken { get; set; }
    public string RefreshToken { get; set; }

    public RefreshAuthenticationModel(string expiredAccessToken, string refreshToken)
    {
        ExpiredAccessToken = expiredAccessToken;
        RefreshToken = refreshToken;
    }
}
