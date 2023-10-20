using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;

namespace ApiClient.Abstractions
{
    public interface IPersonEndpoints
    {
        Task<Result<PersonDto>> RetrieveLoggedInUserDetailsAsync(AuthenticationUserModel loginUser);
    }
}