using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class DetailDesignEndpoints : IDetailDesignEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<DetailDesignEndpoints> _logger;

    public DetailDesignEndpoints(Utilities utilities, ILogger<DetailDesignEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<ElectricVtolDesignDto>> CreateElectricVtolDesign(CreateDetailedDesign detailedDesign)
    {
        string apiEndpoint = "/api/DetailDesign";

        var result = await _utilities.PostCommandAsync<CreateDetailedDesign, ElectricVtolDesignDto>(detailedDesign, apiEndpoint);

        if (result.IsFailed)
        {
            return Result.Fail("Could not create a detail design of the eVTOL");
        }

        return result;
    }
}
