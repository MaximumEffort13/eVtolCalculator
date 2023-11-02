using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions
{
    public interface IInverterEndpoints
    {
        Task<Result<InverterDto>> CreateInverterAsync(CreateInverterModel blade);
    }
}