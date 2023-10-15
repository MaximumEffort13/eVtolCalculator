using Application.Abstractions;
using Application.DTO;
using Application.InverterFacilitators.Queries;
using Application.Mappers;
using Domain.Abstractions;
using FluentResults;

namespace Application.InverterFacilitators.Handlers;
internal class GetInverterByNameQueryHandler : IQueryHandler<GetInverterByNameQuery, InverterDto>
{
    private readonly IInvererRepository _invererRepository;

    public GetInverterByNameQueryHandler(IInvererRepository invererRepository)
    {
        _invererRepository = invererRepository;
    }

    public async Task<Result<InverterDto>> Handle(GetInverterByNameQuery request, CancellationToken cancellationToken)
    {
        var inverter = await _invererRepository.GetByNameAsync(request.Name, cancellationToken);

        var response = InverterDtoMapper.Map(inverter);

        return response;
    }
}
