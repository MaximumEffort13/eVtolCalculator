using Domain.EntityCalculations;
using Domain.Primitives;

namespace Domain.Entities.ConceptDesign;

public sealed class MissionParameterEstimates : Entity
{
    public MissionParameterEstimates(Guid id,
        Guid userId,
        MeasureandQuantity totalDesignWeight,
        MeasureandQuantity payloadWeight,
        TimeSpan flightTimeRequirementInMinutes) : base(id)
    {
        TotalDesignWeight = totalDesignWeight;
        PayloadWeight = payloadWeight;
        FlightTimeRequirementInMinutes = flightTimeRequirementInMinutes;

        EstimatedPowerRequirement = DesignConstraintsCalculations.CalculatePowerRequirement(totalDesignWeight, payloadWeight);
        EstimatedBatteryCapacityRequirement = DesignConstraintsCalculations.CalculateBatteryCapacityRequired(EstimatedPowerRequirement, flightTimeRequirementInMinutes);
        EstimatedBatteryWeight = DesignConstraintsCalculations.CalculateEstimatedBatteryWeight(EstimatedBatteryCapacityRequirement);
        EstimatedMotorWeight = DesignConstraintsCalculations.CalculateEstimatedMotorWeight(EstimatedPowerRequirement);
        EstimatedHorsepowerRequiredForHover = DesignConstraintsCalculations.CalculateHorsepower(EstimatedPowerRequirement);
        UserId = userId;
    }

    public Guid UserId { get; private set; }
    public MeasureandQuantity TotalDesignWeight { get; private set; }
    public MeasureandQuantity PayloadWeight { get; private set; }
    public TimeSpan FlightTimeRequirementInMinutes { get; private set; }
    public MeasureandQuantity EstimatedPowerRequirement { get; private set; }
    public MeasureandQuantity EstimatedBatteryCapacityRequirement { get; private set; }
    public MeasureandQuantity EstimatedBatteryWeight { get; private set; }
    public MeasureandQuantity EstimatedMotorWeight { get; private set; }
    public MeasureandQuantity EstimatedHorsepowerRequiredForHover { get; private set; }
}