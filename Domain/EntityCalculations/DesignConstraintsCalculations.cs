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

        return new MeasureandQuantity(Math.Round(powerRequirement, 4), SiPrefixes.Kilo.Name + SiUnits.Watt.Name);
    }

    public static MeasureandQuantity CalculateBatteryCapacityRequired(MeasureandQuantity powerRequirement, TimeSpan flightTimeRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(normalisedPower * flightTimeRequirement.TotalHours, 4), SiUnits.WattHour);
    }

    public static MeasureandQuantity CalculateEstimatedBatteryWeight(MeasureandQuantity capacityRequirement)
    {
        var normalisedCapacity = SiPrefixes.NormaliseValue(capacityRequirement, SiUnits.WattHour);

        return new MeasureandQuantity(Math.Round(normalisedCapacity / PredefinedConstantValues.currentBatteryCapacityPerKg, 4), SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    public static MeasureandQuantity CalculateEstimatedMotorWeight(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);
        var scaleToKiloWatt = SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedPower, SiUnits.Watt);

        return new MeasureandQuantity(Math.Round(scaleToKiloWatt.Value / PredefinedConstantValues.motorPowerLoading, 4), SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    public static MeasureandQuantity CalculateHorsepower(MeasureandQuantity powerRequirement)
    {
        var normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);

        return new MeasureandQuantity(Math.Round(normalisedPower / PredefinedConstantValues.horsepowerToWatConversion, 4), SiUnits.Horsepower.Name);
    }
}
