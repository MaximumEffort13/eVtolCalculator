using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public sealed class ElectricCalculations
{
    public static MeasureandQuantity CalculateVoltageFromUnitConnectionCount(MeasureandQuantity voltageRating, int numberOfUnitsConnectedInSeries)
    {
        double normalisedVoltage = 0;

        if (voltageRating.Unit != null && voltageRating.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltageRating.Unit[0]);

            normalisedVoltage = voltageRating.Value * prefix.Value;
        }

        var voltage = normalisedVoltage * numberOfUnitsConnectedInSeries;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(voltage, SiUnits.Voltage);
    }

    public static MeasureandQuantity CalculateCurrentFromUnitConnectionCount(MeasureandQuantity current, int numberOfUnitsConnectedInParallel)
    {
        double normalisedCurrent = 0;

        if (current.Unit != null && current.Unit.StartsWith(SiUnits.Current.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(current.Unit[0]);

            normalisedCurrent = current.Value * prefix.Value;
        }

        var newCurrent = normalisedCurrent * numberOfUnitsConnectedInParallel;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(newCurrent, SiUnits.Current);
    }

    public static MeasureandQuantity CalculateCapacityBaseOnUnitConnections(MeasureandQuantity capacityOfUnit, int connectionsInParallel)
    {
        double normalisedCapacity = 0;

        if (capacityOfUnit.Unit != null && capacityOfUnit.Unit.StartsWith(SiUnits.WattHour.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(capacityOfUnit.Unit[0]);

            normalisedCapacity = capacityOfUnit.Value * prefix.Value;
        }

        var capacity = normalisedCapacity * connectionsInParallel;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(capacity, SiUnits.WattHour);
    }

    public static MeasureandQuantity CalculatePower(MeasureandQuantity voltage, MeasureandQuantity current)
    {
        double normalisedCurrent = 0;
        double normalisedVoltage = 0;

        if (current.Unit != null && current.Unit.StartsWith(SiUnits.Current.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(current.Unit[0]);

            normalisedCurrent = current.Value * prefix.Value;
        }

        if (voltage.Unit != null && voltage.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltage.Unit[0]);

            normalisedVoltage = voltage.Value * prefix.Value;
        }

        var power = normalisedVoltage * normalisedCurrent;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(power, SiUnits.Watt);
    }


    public static MeasureandQuantity CalculateSpecificEnergy(MeasureandQuantity capacity, MeasureandQuantity weight)
    {
        double normalisedCapacity = 0;
        double newWeight = 0;
        string weightUnit = string.Empty;

        if (capacity.Unit != null && capacity.Unit.StartsWith(SiUnits.Watt.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(capacity.Unit[0]);

            normalisedCapacity = capacity.Value * prefix.Value;
        }

        if (weight.Unit != null && weight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(weight.Unit[0]);

            var normalisedWeight = weight.Value * prefix.Value;

            newWeight = normalisedWeight * SiPrefixes.Kilo.Value;
            weightUnit = $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}";
        }

        var specificEnergy= SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedCapacity / newWeight, SiUnits.WattHour);

        return new MeasureandQuantity(specificEnergy.Value, $"{specificEnergy.Unit}/{weightUnit}");
    }

    public static MeasureandQuantity CalculatePowerToWeightRatio(MeasureandQuantity voltage, MeasureandQuantity curent, MeasureandQuantity weight)
    {
        var power = CalculatePower(voltage, curent);

        double newWeight = 0;
        string weightUnit = string.Empty;

        if (weight.Unit != null && weight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(weight.Unit[0]);

            var normalisedWeight = weight.Value * prefix.Value;

            newWeight = normalisedWeight * SiPrefixes.Kilo.Value;
            weightUnit = $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}";
        }

        return new MeasureandQuantity(power.Value / newWeight,$"{power.Unit}/{weightUnit}");
    }
}
