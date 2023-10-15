using Application.DTO;
using Domain.Entities.DetailedDesign.Battery;

namespace Application.Mappers;

public static class BatteryPackDtoMapper
{
    public static BatteryPackDto Map(BatteryPack batteryPack)
    {
        return new BatteryPackDto
        {
            Id = batteryPack.Id,
            Name = batteryPack.Name,
            Capacity = $"{batteryPack.Capacity.Value} {batteryPack.Capacity.Unit}",
            Voltage = $"{batteryPack.Voltage.Value} {batteryPack.Voltage.Unit}",
            Current = $"{batteryPack.Current.Value} {batteryPack.Current.Unit}",
            Weight = $"{batteryPack.Weight.Value} {batteryPack.Weight.Unit}",
            SpecificEnergy = $"{batteryPack.SpecificEnergy.Value} {batteryPack.SpecificEnergy.Unit}",
        };
    }
}
