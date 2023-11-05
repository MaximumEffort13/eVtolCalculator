using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.Handlers.DetailDesign;
internal class GetDetailDesignByNameQueryHandler : IQueryHandler<GetDetailDesignByNameQuery, ElectricVtolDesignDto>
{
    private readonly IElectricVtolRepository _electricVtolRepository;

    public GetDetailDesignByNameQueryHandler(IElectricVtolRepository electricVtolRepository)
    {
        _electricVtolRepository = electricVtolRepository;
    }

    public async Task<Result<ElectricVtolDesignDto>> Handle(GetDetailDesignByNameQuery request, CancellationToken cancellationToken)
    {
        var electricVtolDesign = await _electricVtolRepository.GetByNameAsync(request.Name, cancellationToken);

        var response = ElectricVtolDesignDtoMapper.Map(electricVtolDesign);

        return response;
    }
}
