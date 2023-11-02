using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace ApiClient.Endpoints;

public class ConceptualDesignEndpoints : IConceptualDesignEndpoints
{
    private readonly Utilities _utilities;
    private readonly ILogger<ConceptualDesignEndpoints> _logger;

    public ConceptualDesignEndpoints(Utilities utilities, ILogger<ConceptualDesignEndpoints> logger)
    {
        _utilities = utilities;
        _logger = logger;
    }

    public async Task<Result<ConceptualDesignDto>> CreateConceptualDesign(CreateConceptualDesignModel conceptualDesign)
    {
        string apiEndpoint = "/api/createConceptualDesign";
        var result = await _utilities.PostCommandAsync<CreateConceptualDesignModel, ConceptualDesignDto>(conceptualDesign, apiEndpoint);

        if (result is null)
        {
            return Result.Fail("Could not create new conceptual design.");
        }

        return result;
    }


}
