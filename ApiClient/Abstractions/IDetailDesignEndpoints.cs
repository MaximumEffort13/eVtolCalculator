using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IDetailDesignEndpoints
{
    Task<Result<ElectricVtolDesignDto>> CreateElectricVtolDesign(CreateDetailedDesign detailedDesign);
    Task<Result<List<ElectricVtolDesignDto>>> GetAllDetailDesigns();
    Task<Result<ElectricVtolDesignDto>> GetDetailDesignByIdAsync(string id);
}