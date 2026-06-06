using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Battery;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Battery;
internal class GetBatteryPackByNameQueryHandler : IQueryHandler<GetBatteryPackByNameQuery, BatteryPackDto>
{
    private readonly IBatteryPackRepository _batteryPackRepository;

    public GetBatteryPackByNameQueryHandler(IBatteryPackRepository batteryPackRepository)
    {
        _batteryPackRepository = batteryPackRepository;
    }

    public async Task<Result<BatteryPackDto>> Handle(GetBatteryPackByNameQuery request, CancellationToken cancellationToken)
    {
        var battery = await _batteryPackRepository.GetByNameAsync(request.Name, request.UserId, cancellationToken);

        var response = BatteryPackDtoMapper.Map(battery);

        return response;
    }
}
