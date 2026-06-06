using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.ConceptualDesign;

public sealed record GetAllConceptualDesignsQuery(Guid UserId) : IQuery<List<ConceptualDesignDto>>;