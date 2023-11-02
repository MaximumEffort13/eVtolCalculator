using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.Handlers.DetailDesign;

internal sealed class GetAllDetailDesignsQueryHandler : IQueryHandler<GetAllDetailDesignsQuery, IEnumerable<ElectricVtolDesignDto>>
{
    private readonly IElectricVtolRepository _electricVtolRepository;

    public GetAllDetailDesignsQueryHandler(IElectricVtolRepository electricVtolRepository)
    {
        _electricVtolRepository = electricVtolRepository;
    }

    public async Task<Result<IEnumerable<ElectricVtolDesignDto>>> Handle(GetAllDetailDesignsQuery request, CancellationToken cancellationToken)
    {
        var electricVtolDesigns = await _electricVtolRepository.GetAllAsync(cancellationToken);

        List<ElectricVtolDesignDto> electricVtolDesignDtos = new();

        electricVtolDesigns.ForEach(a => electricVtolDesignDtos.Add(ElectricVtolDesignDtoMapper.Map(a)));

        return electricVtolDesignDtos;
    }
}
