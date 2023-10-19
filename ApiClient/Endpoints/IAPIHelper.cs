using ApiClient.DataTransferObjects;

namespace ApiClient.Endpoints;

public interface IAPIHelper
{
    void LogOffUser();
    Task<AuthenticatedUser> Authenticate(string username, string password);
    Task GetLoggedInUserInfo(string token);
}