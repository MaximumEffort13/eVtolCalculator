using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.ConceptualDesign;

public sealed record GetConceptualDesignByIdQuery(Guid designId) : IQuery<ConceptualDesignDto>;

