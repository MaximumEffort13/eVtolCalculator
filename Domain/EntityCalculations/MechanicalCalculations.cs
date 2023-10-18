using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public static  class MechanicalCalculations
{
    public static MeasureandQuantity CalculateBatteryModuleWeight(MeasureandQuantity cellWeight, int numberOfCellsInModule)
    {
        double normalisedWeight = 0.0;

        if (cellWeight.Unit is not null && cellWeight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(cellWeight.Unit[0]);

            normalisedWeight = cellWeight.Value * prefix.Value;
        }

        var moduleWeight = normalisedWeight * numberOfCellsInModule;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(moduleWeight, SiUnits.Mass);
    }

    public static MeasureandQuantity CalculateBatteryPackWeightUsingBatteryModule(MeasureandQuantity moduleWeight, int numberOfModulesInPack)
    {
        double normalisedWeight = 0.0;

        if (moduleWeight.Unit is not null && moduleWeight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(moduleWeight.Unit[0]);

            normalisedWeight = moduleWeight.Value * prefix.Value;
        }

        var packWeight = normalisedWeight * numberOfModulesInPack;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(packWeight, SiUnits.Mass);
    }


    public static MeasureandQuantity CalculateRpm(double kv, MeasureandQuantity voltageRating)
    {
        double normalisedVoltage = 0;

        if (voltageRating.Unit != null && voltageRating.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltageRating.Unit[0]);

            normalisedVoltage = voltageRating.Value * prefix.Value;
        }

        var rpmPerVolt = normalisedVoltage * kv;

        return new MeasureandQuantity(rpmPerVolt, $"{SiUnits.Rpm.Name}/{SiUnits.Voltage.Name}");
    }


    public static MeasureandQuantity CalculateLiftOffWeight(
        MeasureandQuantity payloadWeight,
        MeasureandQuantity batteryWeight,
        MeasureandQuantity motorWeight,
        MeasureandQuantity inverterWeight,
        MeasureandQuantity bladeWeight,
        MeasureandQuantity fuselageWeight,
        int motorQuantity,
        int bladesPerMotorQuantity)
    {
        var normalisedPayloadWeight = SiPrefixes.NormaliseValue(payloadWeight, SiUnits.Mass);
        var normalisedBatteryWeight = SiPrefixes.NormaliseValue(batteryWeight, SiUnits.Mass);
        var normalisedMotorWeight = SiPrefixes.NormaliseValue(motorWeight, SiUnits.Mass);
        var normalisedInverterWeight = SiPrefixes.NormaliseValue(inverterWeight, SiUnits.Mass);
        var normalisedBladeWeight = SiPrefixes.NormaliseValue(bladeWeight, SiUnits.Mass);
        var normalisedFuselageWeight = SiPrefixes.NormaliseValue(fuselageWeight, SiUnits.Mass);

        var totalWeight = normalisedPayloadWeight
                          + normalisedBatteryWeight
                          + (normalisedMotorWeight * motorQuantity)
                          + (normalisedInverterWeight * motorQuantity)
                          + (normalisedBladeWeight * motorQuantity * bladesPerMotorQuantity)
                          + normalisedFuselageWeight;



        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(totalWeight, SiUnits.Mass);
    }
}
