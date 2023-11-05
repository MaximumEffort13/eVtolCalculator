using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Battery;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Battery;

internal sealed class GetAllBatteryPackQueryHandler : IQueryHandler<GetAllBatteryPacksQuery, List<BatteryPackDto>>
{
    private readonly IBatteryPackRepository _batteryPackRepository;

    public GetAllBatteryPackQueryHandler(IBatteryPackRepository batteryPackRepository)
    {
        _batteryPackRepository = batteryPackRepository;
    }


    public async Task<Result<List<BatteryPackDto>>> Handle(GetAllBatteryPacksQuery request, CancellationToken cancellationToken)
    {
        var batteries = await _batteryPackRepository.GetAllAsync(cancellationToken);

        List<BatteryPackDto> batteryDto = new ();

        batteries.ForEach(b =>  batteryDto.Add(BatteryPackDtoMapper.Map(b)));

        return batteryDto;
    }
}
