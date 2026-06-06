using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class InverterDtoMapper
{
    public static InverterDto Map(InverterEntity inverter)
    {
        return new InverterDto
        {
            Id = inverter.Id.ToString(),
            Name = inverter.Name,
            VoltageRating = $"{inverter.VoltageRating.Value} {inverter.VoltageRating.Unit}",
            CurrentRating = $"{inverter.CurrentRating.Value} {inverter.CurrentRating.Unit}",
            Weight = $"{inverter.Weight.Value} {inverter.Weight.Unit}",
            PowerToWeightRatio = $"{inverter.PowerToWeightRatio.Value} {inverter.PowerToWeightRatio.Unit}"
        };
    }
}
