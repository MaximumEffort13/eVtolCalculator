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

        var powerRequirement = (normalisedStructureWeight + normalisedPayloadWeight) * SiPrefixes.Kilo.Value / PredefinedConstantValues.motorThrustToPowerRatio;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(powerRequirement, SiUnits.Watt);
    }

    public static MeasureandQuantity CalculateBatteryCapacityRequired(MeasureandQuantity powerRequirement, TimeSpan flightTimeRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedPower * flightTimeRequirement.TotalHours, SiUnits.WattHour);
    }

    public static MeasureandQuantity CalculateEstimatedBatteryWeight(MeasureandQuantity capacityRequirement)
    {
        var normalisedCapacity = SiPrefixes.NormaliseValue(capacityRequirement, SiUnits.WattHour);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedCapacity / PredefinedConstantValues.currentBatteryCapacityPerKg, SiUnits.Mass);
    }

    public static MeasureandQuantity CalculateEstimatedMotorWeight(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);
        var scaleToKiloWatt = normalisedPower * SiPrefixes.Kilo.Value;

        return new MeasureandQuantity(scaleToKiloWatt / PredefinedConstantValues.motorPowerLoading, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    public static MeasureandQuantity CalculateHorsepower(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedPower / PredefinedConstantValues.horsepowerToWatConversion, SiUnits.Horsepower);
    }
}
