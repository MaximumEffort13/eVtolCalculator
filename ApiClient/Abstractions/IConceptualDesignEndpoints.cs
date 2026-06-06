using ApiClient.DataTransferObjects.ApiRequests;
using ApiClient.DataTransferObjects.ApiResponses;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IConceptualDesignEndpoints
{
    Task<Result<ConceptualDesignDto>> CreateConceptualDesign(CreateConceptualDesignModel conceptualDesign);
    Task<Result<List<ConceptualDesignDto>>> GetConceptualDesigns();
    Task<Result<ConceptualDesignDto>> GetConceptualDesignByIdAsync(string id);
}