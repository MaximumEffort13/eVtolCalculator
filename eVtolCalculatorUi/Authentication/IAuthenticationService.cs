using ApiClient.DataTransferObjects.IdentityObjects;

namespace eVtolCalculatorUi.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
    Task<AuthenticatedUserModel> Refresh();
    Task Logout();
}