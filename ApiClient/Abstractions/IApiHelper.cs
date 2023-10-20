using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;

namespace ApiClient.Abstractions
{
    public interface IApiHelper
    {
        HttpClient Client { get; }

        Task<Result<AuthenticatedUserModel>> Authenticate(AuthenticationUserModel loginUser);
        Task<Result> GetLoggedInUserInfo(string token);
        void Logout();
        Task<Result<AuthenticatedUserModel>> RefreshLogin(string refreshToken);
        void UnothorizeClient();
    }
}