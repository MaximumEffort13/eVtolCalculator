using Domain.Enums;
using Domain.Primitives;
using System.Text.RegularExpressions;

namespace Domain.EntityCalculations;

public sealed class ElectricCalculations
{
    public static MeasureandQuantity CalculateVoltageFromUnitConnectionCount(MeasureandQuantity voltageRating, int numberOfUnitsConnectedInSeries)
    {
        MeasureandQuantity[] inputs = { voltageRating };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedVoltage = voltageRating.Value;

        if (voltageRating.Unit != null && voltageRating.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltageRating.Unit[0]);

            normalisedVoltage = voltageRating.Value * prefix.Value;
        }

        var voltage = normalisedVoltage * numberOfUnitsConnectedInSeries;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(voltage,4), SiUnits.Voltage);
    }

    public static MeasureandQuantity CalculateCurrentFromUnitConnectionCount(MeasureandQuantity current, int numberOfUnitsConnectedInParallel)
    {
        MeasureandQuantity[] inputs = { current };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedCurrent = current.Value;

        if (current.Unit != null && current.Unit.StartsWith(SiUnits.Current.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(current.Unit[0]);

            normalisedCurrent = current.Value * prefix.Value;
        }

        var newCurrent = normalisedCurrent * numberOfUnitsConnectedInParallel;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(newCurrent,4), SiUnits.Current);
    }

    public static MeasureandQuantity CalculateCapacityBaseOnUnitConnections(MeasureandQuantity capacityOfUnit, int connectionsInParallel)
    {
        MeasureandQuantity[] inputs = { capacityOfUnit };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedCapacity = capacityOfUnit.Value;

        if (capacityOfUnit.Unit != null && capacityOfUnit.Unit.StartsWith(SiUnits.AmpHour.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(capacityOfUnit.Unit[0]);

            normalisedCapacity = capacityOfUnit.Value * prefix.Value;
        }

        var capacity = normalisedCapacity * connectionsInParallel;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(capacity, 4), SiUnits.AmpHour);
    }

    public static MeasureandQuantity CalculatePower(MeasureandQuantity voltage, MeasureandQuantity current)
    {
        MeasureandQuantity[] inputs = { voltage, current };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedCurrent = current.Value;
        double normalisedVoltage = voltage.Value;

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

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(power, 4), SiUnits.Watt);
    }

    public static MeasureandQuantity CalculateEnergy(MeasureandQuantity voltage, MeasureandQuantity capacity)
    {
        MeasureandQuantity[] inputs = { voltage, capacity };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedCapacity = capacity.Value;
        double normalisedVoltage = voltage.Value;

        if (capacity.Unit != null && capacity.Unit.StartsWith(SiUnits.AmpHour.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(capacity.Unit[0]);

            normalisedCapacity = capacity.Value * prefix.Value;
        }

        if (voltage.Unit != null && voltage.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltage.Unit[0]);

            normalisedVoltage = voltage.Value * prefix.Value;
        }

        var energy = normalisedVoltage * normalisedCapacity;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(energy, 4), SiUnits.WattHour);

    }

    public static MeasureandQuantity CalculateSpecificEnergy(MeasureandQuantity energy, MeasureandQuantity mass)
    {
        MeasureandQuantity[] inputs = { energy, mass };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        double normalisedCapacity = energy.Value;

        if (energy.Unit != null && energy.Unit.StartsWith(SiUnits.WattHour.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(energy.Unit[0]);

            normalisedCapacity = energy.Value * prefix.Value;
        }

        string massUnit = mass.Unit;
        double netMass = mass.Value;

        if (mass.Unit != null && mass.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(mass.Unit[0]);

            var normalisedWeight = mass.Value * prefix.Value;

            netMass = normalisedWeight / SiPrefixes.Kilo.Value;
            massUnit = $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}";
        }

        var specificEnergy= SiPrefixes.ScaleNormalisedValueToAppropriateUnit(normalisedCapacity / netMass, SiUnits.WattHour);

        return new MeasureandQuantity(Math.Round(specificEnergy.Value, 4), $"{specificEnergy.Unit}/{massUnit}");
    }

    public static MeasureandQuantity CalculatePowerToWeightRatio(MeasureandQuantity voltage, MeasureandQuantity current, MeasureandQuantity weight)
    {
        MeasureandQuantity[] inputs = { voltage, current, weight };

        if (ValidInput(inputs) == false)
        {
            return new MeasureandQuantity(0, string.Empty);
        }

        var power = CalculatePower(voltage, current);

        double newWeight = weight.Value;
        string weightUnit = weight.Unit;

        if (weight.Unit != null && weight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(weight.Unit[0]);

            var normalisedWeight = weight.Value * prefix.Value;

            newWeight = normalisedWeight / SiPrefixes.Kilo.Value;
            weightUnit = $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}";
        }

        return new MeasureandQuantity(Math.Round(power.Value / newWeight, 4),$"{power.Unit}/{weightUnit}");
    }

    private static bool ValidInput(MeasureandQuantity[] values)
    {
        foreach (MeasureandQuantity value in values)
        {
            if (value is null || value.Value is 0 || value.Unit is null)
            {
                return false;
            }
        }

        return true;
    }
}
