using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.Handlers.DetailDesign;
internal class GetDetailDesignByIdQueryHandler : IQueryHandler<GetDetailDesignByIdQuery, ElectricVtolDesignDto>
{
    private readonly IElectricVtolRepository _electricVtolRepository;

    public GetDetailDesignByIdQueryHandler(IElectricVtolRepository electricVtolRepository)
    {
        _electricVtolRepository = electricVtolRepository;
    }

    public async Task<Result<ElectricVtolDesignDto>> Handle(GetDetailDesignByIdQuery request, CancellationToken cancellationToken)
    {
        var electricVtolDesign = await _electricVtolRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = ElectricVtolDesignDtoMapper.Map(electricVtolDesign);

        return response;
    }
}
