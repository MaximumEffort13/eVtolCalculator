using Application.Abstractions;
using Application.BatteryFacilitators.Queries;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using FluentResults;

namespace Application.BatteryFacilitators.Handlers;
internal class GetBatteryPackByIdQueryHandler : IQueryHandler<GetBatteryPackByIdQuery, BatteryPackDto>
{
    private readonly IBatteryPackRepository _batteryPackRepository;

    public GetBatteryPackByIdQueryHandler(IBatteryPackRepository batteryPackRepository)
    {
        _batteryPackRepository = batteryPackRepository;
    }

    public async Task<Result<BatteryPackDto>> Handle(GetBatteryPackByIdQuery request, CancellationToken cancellationToken)
    {
        var battery = await _batteryPackRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = BatteryPackDtoMapper.Map(battery);

        return response;
    }
}
