namespace eVtolCalculatorApi.Models;

internal class TokenDto
{
    public string AccessToken { get; }
    public string UserName { get; }
    public string RefreshToken { get; }
    public int AccessTokenExpInMin { get; }
    public int RefreshTokenExpInDays { get; }

    public TokenDto(string accessToken, string userName, string refreshToken, int accessTokenExpInMin, int refreshTokenExpInDays)
    {
        AccessToken = accessToken;
        UserName = userName;
        RefreshToken = refreshToken;
        AccessTokenExpInMin = accessTokenExpInMin;
        RefreshTokenExpInDays = refreshTokenExpInDays;
    }

    public override bool Equals(object? obj)
    {
        return obj is TokenDto other &&
               AccessToken == other.AccessToken &&
               UserName == other.UserName &&
               RefreshToken == other.RefreshToken &&
               AccessTokenExpInMin == other.AccessTokenExpInMin &&
               RefreshTokenExpInDays == other.RefreshTokenExpInDays;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AccessToken, UserName, RefreshToken, AccessTokenExpInMin, RefreshTokenExpInDays);
    }
}