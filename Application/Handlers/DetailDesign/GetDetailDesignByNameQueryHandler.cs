using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using Domain.Abstractions;
using FluentResults;

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
        var electricVtolDesign = await _electricVtolRepository.GetByNameAsync(request.Name, request.UserId, cancellationToken);

        var response = ElectricVtolDesignDtoMapper.Map(electricVtolDesign);

        return response;
    }
}
