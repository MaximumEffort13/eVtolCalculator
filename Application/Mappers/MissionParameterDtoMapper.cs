using Application.DTO;
using Domain.Entities.ConceptDesign;

namespace Application.Mappers;

public static class MissionParameterDtoMapper
{
    public static MissionParameterDto Map(MissionParameterEstimates missionParameters)
    {
        return new MissionParameterDto
        {
            EstimatedPowerRequirement = $"{missionParameters.EstimatedPowerRequirement.Value} {missionParameters.EstimatedPowerRequirement.Unit}",
            EstimatedBatteryCapacityRequirement = $"{missionParameters.EstimatedBatteryCapacityRequirement.Value} {missionParameters.EstimatedBatteryCapacityRequirement.Unit}",
            EstimatedBatteryWeight = $"{missionParameters.EstimatedBatteryWeight.Value} {missionParameters.EstimatedBatteryWeight.Unit}",
            EstimatedMotorWeight = $"{missionParameters.EstimatedMotorWeight.Value} {missionParameters.EstimatedMotorWeight.Unit}",
            EstimatedHorsepowerRequiredForHover = $"{missionParameters.EstimatedHorsepowerRequiredForHover.Value} {missionParameters.EstimatedHorsepowerRequiredForHover.Unit}"
        };
    }
}
