using Application.Abstractions;
using Application.DTO;
using Application.InverterFacilitators.Queries;
using Application.Mappers;
using Domain.Abstractions;
using FluentResults;

namespace Application.InverterFacilitators.Handlers;
internal class GetInverterByIdQueryHandler : IQueryHandler<GetInverterByIdQuery, InverterDto>
{
    private readonly IInvererRepository _invererRepository;

    public GetInverterByIdQueryHandler(IInvererRepository invererRepository)
    {
        _invererRepository = invererRepository;
    }

    public async Task<Result<InverterDto>> Handle(GetInverterByIdQuery request, CancellationToken cancellationToken)
    {
        var inverter = await _invererRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = InverterDtoMapper.Map(inverter);

        return response;
    }
}
