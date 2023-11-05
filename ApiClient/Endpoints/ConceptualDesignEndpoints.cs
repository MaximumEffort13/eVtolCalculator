using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using ApiClient.Enums;
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
        var result = await _utilities.PostCommandAsync<CreateConceptualDesignModel, ConceptualDesignDto>(conceptualDesign, ConceptualDesignRoutes.Create.Name);

        if (result is null)
        {
            return Result.Fail(result!.Reasons.Last().ToString());
        }

        return result;
    }

    public async Task<Result<List<ConceptualDesignDto>>> GetConceptualDesigns()
    {
        var results = await _utilities.GetRequestAsync<List<ConceptualDesignDto>>(ConceptualDesignRoutes.GetAll.Name);

        if (results is null || results.IsFailed)
        {
            return Result.Fail(results!.Reasons.Last().ToString());
        }

        return results;
    }

}
