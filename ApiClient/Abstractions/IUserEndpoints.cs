using ApiClient.Models;
using FluentResults;

namespace ApiClient.Abstractions
{
    public interface IUserEndpoints
    {
        Task<Result> RegisterUser(CreateUserModel user);
    }
}