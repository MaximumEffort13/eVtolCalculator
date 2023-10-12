using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class InverterDtoMapper
{
    public static InverterDto Map(Inverter inverter)
    {
        return new InverterDto
        {
            VoltageRating = $"{inverter.VoltageRating.Value} {inverter.VoltageRating.Unit}",
            CurrentRating = $"{inverter.CurrentRating.Value} {inverter.CurrentRating.Unit}",
            Weight = $"{inverter.Weight.Value} {inverter.Weight.Unit}",
            PowerToWeightRatio = $"{inverter.PowerToWeightRatio.Value} {inverter.PowerToWeightRatio.Unit}"
        };
    }
}
