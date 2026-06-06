using Domain.ConstantValues;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.ConceptDesign;

public sealed class ConceptualVtolDesign : Entity
{
    public ConceptualVtolDesign(Guid id, Guid userId, string name, MeasureandQuantity totalDesignWeight, MeasureandQuantity payloadWeight, TimeSpan flightTimeRequirementInMinutes) : base(id)
    {
        UserId = userId;
        Name = name;
        TotalDesignWeight = totalDesignWeight;
        PayloadWeight = payloadWeight;
        FlightTimeRequirementInMinutes = flightTimeRequirementInMinutes;

        PowerRequirement = CalculatePowerRequirement();
        BatteryCapacityRequirement = CalculateBatteryCapacityRequired();
        BatteryWeight = CalculateEstimatedBatteryWeight();
        MotorWeight = CalculateEstimatedMotorWeight();
        Horsepower = CalculateHorsepower();
    }

    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public MeasureandQuantity TotalDesignWeight { get; private set; }
    public MeasureandQuantity PayloadWeight { get; private set; }
    public TimeSpan FlightTimeRequirementInMinutes { get; private set; }
    public MeasureandQuantity PowerRequirement { get; private set; }
    public MeasureandQuantity BatteryCapacityRequirement { get; private set; }
    public MeasureandQuantity BatteryWeight { get; private set; }
    public MeasureandQuantity MotorWeight { get; private set; }
    public MeasureandQuantity Horsepower { get; private set; }

    private MeasureandQuantity CalculatePowerRequirement()
    {
        return new MeasureandQuantity(Math.Round((TotalDesignWeight.Value + PayloadWeight.Value) / PredefinedConstantValues.motorThrustToPowerRatio,4), SiPrefixes.Kilo.Name + SiUnits.Watt.Name);
    }

    private MeasureandQuantity CalculateBatteryCapacityRequired()
    {
        return new MeasureandQuantity(Math.Round(PowerRequirement.Value * FlightTimeRequirementInMinutes.TotalHours,4), SiPrefixes.Kilo.Name + SiUnits.WattHour.Name);
    }

    private MeasureandQuantity CalculateEstimatedBatteryWeight()
    {
        return new MeasureandQuantity(BatteryCapacityRequirement.Value * SiPrefixes.Kilo.Value / PredefinedConstantValues.currentBatteryCapacityPerKg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    private MeasureandQuantity CalculateEstimatedMotorWeight()
    {
        return new MeasureandQuantity(Math.Round(PowerRequirement.Value / PredefinedConstantValues.motorPowerLoading, 4), SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    private MeasureandQuantity CalculateHorsepower()
    {
        return new MeasureandQuantity(Math.Round(PowerRequirement.Value * SiPrefixes.Kilo.Value / PredefinedConstantValues.horsepowerToWatConversion, 4), SiUnits.Horsepower.Name);
    }

}