using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Inverter;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Inverter;
internal class GetInverterByNameQueryHandler : IQueryHandler<GetInverterByNameQuery, InverterDto>
{
    private readonly IInvererRepository _invererRepository;

    public GetInverterByNameQueryHandler(IInvererRepository invererRepository)
    {
        _invererRepository = invererRepository;
    }

    public async Task<Result<InverterDto>> Handle(GetInverterByNameQuery request, CancellationToken cancellationToken)
    {
        var inverter = await _invererRepository.GetByNameAsync(request.Name, request.UserId, cancellationToken);

        var response = InverterDtoMapper.Map(inverter);

        return response;
    }
}
