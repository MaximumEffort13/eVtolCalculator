using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IApiHelper
{
    HttpClient Client { get; }

    Task<Result<AuthenticatedUserModel>> Authenticate(AuthenticationUserModel userForAuthentication);
    Task<Result<AuthenticatedUserModel>> RefreshAuthentication(string accessToken, string refreshToken);
    Task<Result> GetLoggedInUserInfo(string token);
    void Logout();
}