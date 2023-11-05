using Domain.ConstantValues;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public sealed class DesignConstraintsCalculations
{
    public static MeasureandQuantity CalculatePowerRequirement(MeasureandQuantity structureWeight, MeasureandQuantity payloadWeight)
    {
        double normalisedStructureWeight = SiPrefixes.NormaliseValue(structureWeight, SiUnits.Mass);
        double normalisedPayloadWeight = SiPrefixes.NormaliseValue(payloadWeight, SiUnits.Mass);

        var powerRequirement = ((normalisedStructureWeight + normalisedPayloadWeight) / SiPrefixes.Kilo.Value) / PredefinedConstantValues.motorThrustToPowerRatio;

        return new MeasureandQuantity(powerRequirement, SiPrefixes.Kilo.Name + SiUnits.Watt.Name);
    }

    public static MeasureandQuantity CalculateBatteryCapacityRequired(MeasureandQuantity powerRequirement, TimeSpan flightTimeRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedPower * flightTimeRequirement.TotalHours, SiUnits.WattHour);
    }

    public static MeasureandQuantity CalculateEstimatedBatteryWeight(MeasureandQuantity capacityRequirement)
    {
        var normalisedCapacity = SiPrefixes.NormaliseValue(capacityRequirement, SiUnits.WattHour);

        return new MeasureandQuantity(normalisedCapacity / PredefinedConstantValues.currentBatteryCapacityPerKg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    public static MeasureandQuantity CalculateEstimatedMotorWeight(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);
        var scaleToKiloWatt = SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedPower, SiUnits.Watt);

        return new MeasureandQuantity(scaleToKiloWatt.Value / PredefinedConstantValues.motorPowerLoading, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    public static MeasureandQuantity CalculateHorsepower(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return new MeasureandQuantity(normalisedPower / PredefinedConstantValues.horsepowerToWatConversion, SiUnits.Horsepower.Name);
    }
}
