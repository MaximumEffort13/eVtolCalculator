using ApiClient.DataTransferObjects.IdentityObjects;

namespace eVtolCalculatorUi.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
    Task<AuthenticatedUserModel> RefreshAsync();
    Task Logout();
}