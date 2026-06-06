using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IDesignConstantsEndpoints
{
    Task<Result<DesignConstantsDto>> CreateDesingConstants(CreateDesignConstants blade);
    Task<Result<List<DesignConstantsDto>>> GetAllDesignConstants();
    Task<Result<DesignConstantsDto>> GetDesignConstantById(string id);
}