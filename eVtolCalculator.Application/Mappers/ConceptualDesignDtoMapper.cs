using Application.DTO;
using Domain.Entities.ConceptDesign;

namespace Application.Mappers;

public static class ConceptualDesignDtoMapper
{
    public static ConceptualDesignDto Map(ConceptualVtolDesign conceptDesign)
    {
        return new ConceptualDesignDto
        {
            ProwerRequirement = $"{conceptDesign.PowerRequirement.Value} {conceptDesign.PowerRequirement.Unit}",
            BatteryCapacityRequirement = $"{conceptDesign.BatteryCapacityRequirement.Value} {conceptDesign.BatteryCapacityRequirement.Unit}",
            BatteryWeight = $"{conceptDesign.BatteryWeight.Value} {conceptDesign.BatteryWeight.Unit}",
            MotorWeight = $"{conceptDesign.MotorWeight.Value} {conceptDesign.MotorWeight.Unit}",
            Horsepower = $"{conceptDesign.Horsepower.Value} {conceptDesign.Horsepower.Unit}"
        };
    }
}
