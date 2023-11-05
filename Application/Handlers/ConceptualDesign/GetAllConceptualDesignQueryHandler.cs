using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.ConceptualDesign;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.ConceptualDesign;

internal sealed class GetAllConceptualDesignQueryHandler : IQueryHandler<GetAllConceptualDesignsQuery, List<ConceptualDesignDto>>
{
    private readonly IConceptualDesignRepository _conceptualDesignRepository;

    public GetAllConceptualDesignQueryHandler(IConceptualDesignRepository conceptualDesignRepository)
    {
        _conceptualDesignRepository = conceptualDesignRepository;
    }

    public async Task<Result<List<ConceptualDesignDto>>> Handle(GetAllConceptualDesignsQuery request, CancellationToken cancellationToken)
    {
        var concepts = await _conceptualDesignRepository.GetAllAsync(cancellationToken);

        List<ConceptualDesignDto> dtoConcepts = new();

        concepts.ForEach(a => dtoConcepts.Add(ConceptualDesignDtoMapper.Map(a)));

        return dtoConcepts;
    }
}
