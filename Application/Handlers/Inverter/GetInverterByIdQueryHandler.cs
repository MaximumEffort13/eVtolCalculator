using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Inverter;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Inverter;
internal class GetInverterByIdQueryHandler : IQueryHandler<GetInverterByIdQuery, InverterDto>
{
    private readonly IInvererRepository _invererRepository;

    public GetInverterByIdQueryHandler(IInvererRepository invererRepository)
    {
        _invererRepository = invererRepository;
    }

    public async Task<Result<InverterDto>> Handle(GetInverterByIdQuery request, CancellationToken cancellationToken)
    {
        var inverter = await _invererRepository.GetByIdAsync(request.Id, request.UserId, cancellationToken);

        var response = InverterDtoMapper.Map(inverter);

        return response;
    }
}
