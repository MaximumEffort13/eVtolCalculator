using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IBladeEndpoints
{
    Task<Result<BladeDto>> CreateBladeAsync(CreateBladeModel blade);
    Task<Result<List<BladeDto>>> GetBlades();
    Task<Result<BladeDto>> GetBladeById(string id);
}