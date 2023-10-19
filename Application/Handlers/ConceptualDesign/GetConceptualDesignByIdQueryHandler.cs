using FluentResults;
using Application.Abstractions;
using Domain.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.ConceptualDesign;

namespace Application.Handlers.ConceptualDesign;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetConceptualDesignByIdQuery, ConceptualDesignDto>
{
    private readonly IConceptualDesignRepository _overallDesignRepository;

    public GetUserByIdQueryHandler(IConceptualDesignRepository overallDesignRepository)
    {
        _overallDesignRepository = overallDesignRepository;
    }

    public async Task<Result<ConceptualDesignDto>> Handle(GetConceptualDesignByIdQuery request, CancellationToken cancellationToken)
    {
        var design = await _overallDesignRepository.GetByIdAsync(request.designId, cancellationToken);

        if (design is null)
        {
            return Result.Fail<ConceptualDesignDto>($"The design with Id {request.designId} does not exist. Please recreate the design");
        }

        var response = ConceptualDesignDtoMapper.Map(design);

        return response;
    }
}
