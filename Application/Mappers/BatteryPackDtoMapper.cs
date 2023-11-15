using Application.DTO;
using Domain.Entities.DetailedDesign.Battery;

namespace Application.Mappers;

public static class BatteryPackDtoMapper
{
    public static BatteryPackDto Map(BatteryPack batteryPack)
    {
        return new BatteryPackDto
        {
            Id = batteryPack.Id.ToString(),
            Name = batteryPack.Name,
            Capacity = $"{batteryPack.Capacity.Value} {batteryPack.Capacity.Unit}",
            Voltage = $"{batteryPack.Voltage.Value} {batteryPack.Voltage.Unit}",
            Mass = $"{batteryPack.Mass.Value} {batteryPack.Mass.Unit}",
            Energy = $"{batteryPack.Energy.Value} {batteryPack.Energy.Unit}",
            SpecificEnergy = $"{batteryPack.SpecificEnergy.Value} {batteryPack.SpecificEnergy.Unit}",
        };
    }
}
