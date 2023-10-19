using ApiClient.DataTransferObjects;

namespace eVtolCalculatorUi.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
    Task Logout();
}