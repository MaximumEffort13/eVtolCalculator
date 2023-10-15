using Application.Abstractions;
using Application.DetailDesignFacilitators.Queries;
using Application.DTO;
using Application.Mappers;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.DetailDesignFacilitators.Handlers;
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
