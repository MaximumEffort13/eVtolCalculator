using Domain.ConstantValues;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.ConceptDesign;

public sealed class MissionParameterEstimates : Entity
{
    public MissionParameterEstimates(Guid id, MeasureandQuantity totalDesignWeight, MeasureandQuantity payloadWeight, TimeSpan flightTimeRequirementInMinutes) : base(id)
    {
        TotalDesignWeight = totalDesignWeight;
        PayloadWeight = payloadWeight;
        FlightTimeRequirementInMinutes = flightTimeRequirementInMinutes;

        EstimatedPowerRequirement = CalculatePowerRequirement();
        EstimatedBatteryCapacityRequirement = CalculateBatteryCapacityRequired();
        EstimatedBatteryWeight = CalculateEstimatedBatteryWeight();
        EstimatedMotorWeight = CalculateEstimatedMotorWeight();
        EstimatedHorsepowerRequiredForHover = CalculateHorsepower();
    }

    public MeasureandQuantity TotalDesignWeight { get; private set; }
    public MeasureandQuantity PayloadWeight { get; private set; }
    public TimeSpan FlightTimeRequirementInMinutes { get; private set; }
    public MeasureandQuantity EstimatedPowerRequirement { get; private set; }
    public MeasureandQuantity EstimatedBatteryCapacityRequirement { get; private set; }
    public MeasureandQuantity EstimatedBatteryWeight { get; private set; }
    public MeasureandQuantity EstimatedMotorWeight { get; private set; }
    public MeasureandQuantity EstimatedHorsepowerRequiredForHover { get; private set; }

    // Use actual entities for power to weight ratio calculations /////////////////////////////////////////////

    private MeasureandQuantity CalculatePowerRequirement()
    {
        return new MeasureandQuantity((TotalDesignWeight.Value + PayloadWeight.Value) / PredefinedConstantValues.motorThrustToPowerRatio, SiPrefixes.Kilo.Name + SiUnits.Power.Name);
    }

    private MeasureandQuantity CalculateBatteryCapacityRequired()
    {
        return new MeasureandQuantity(EstimatedPowerRequirement.Value * FlightTimeRequirementInMinutes.TotalHours, SiPrefixes.Kilo.Name + SiUnits.WattHour.Name);
    }

    private MeasureandQuantity CalculateEstimatedBatteryWeight()
    {
        return new MeasureandQuantity(EstimatedPowerRequirement.Value * SiPrefixes.Kilo.Value / PredefinedConstantValues.currentBatteryCapacityPerKg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    private MeasureandQuantity CalculateEstimatedMotorWeight()
    {
        return new MeasureandQuantity(EstimatedPowerRequirement.Value / PredefinedConstantValues.motorPowerLoading, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    private MeasureandQuantity CalculateHorsepower()
    {
        return new MeasureandQuantity(EstimatedPowerRequirement.Value * SiPrefixes.Kilo.Value / PredefinedConstantValues.horsepowerToWatConversion, SiUnits.Horsepower.Name);
    }

}