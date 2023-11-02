using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions
{
    public interface IMotorEndpoints
    {
        Task<Result<MotorDto>> CreateMotorAsync(CreateMotorModel blade);
    }
}