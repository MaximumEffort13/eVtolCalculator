using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public static  class MechanicalCalculations
{
    public static MeasureandQuantity CalculateBatteryModuleWeight(MeasureandQuantity cellWeight, int numberOfCellsInModule)
    {
        double normalisedWeight = cellWeight.Value;

        if (cellWeight.Unit is not null && cellWeight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(cellWeight.Unit[0]);

            normalisedWeight = cellWeight.Value * prefix.Value;
        }

        var moduleWeight = normalisedWeight * numberOfCellsInModule;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(moduleWeight, 4), SiUnits.Mass);
    }

    public static MeasureandQuantity CalculateBatteryPackWeightUsingBatteryModule(MeasureandQuantity moduleWeight, int numberOfModulesInPack, MeasureandQuantity miscellaneousPackWeight)
    {
        double normalisedWeight = moduleWeight.Value;
        double normalisedMiscellaneousWeight = miscellaneousPackWeight.Value;

        if (moduleWeight.Unit is not null && moduleWeight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(moduleWeight.Unit[0]);

            normalisedWeight = moduleWeight.Value * prefix.Value;
        }

        if (miscellaneousPackWeight.Unit is not null && miscellaneousPackWeight.Unit.StartsWith(SiUnits.Mass.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(miscellaneousPackWeight.Unit[0]);

            normalisedMiscellaneousWeight = miscellaneousPackWeight.Value * prefix.Value;
        }

        var packWeight = normalisedWeight * numberOfModulesInPack + normalisedMiscellaneousWeight;

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(packWeight, 4), SiUnits.Mass);
    }

    public static MeasureandQuantity CalculateRpm(double kv, MeasureandQuantity voltageRating)
    {
        double normalisedVoltage = voltageRating.Value;

        if (voltageRating.Unit != null && voltageRating.Unit.StartsWith(SiUnits.Voltage.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(voltageRating.Unit[0]);

            normalisedVoltage = voltageRating.Value * prefix.Value;
        }

        var rpmPerVolt = normalisedVoltage * kv;

        return new MeasureandQuantity(Math.Round(rpmPerVolt, 4), $"{SiUnits.Rpm.Name}");
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



        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(totalWeight, 4), SiUnits.Mass);
    }

    public static MeasureandQuantity CalculateTorque(MeasureandQuantity motorCurrent, MeasureandQuantity kvRating)
    {
        var kt = 1 / kvRating.Value;
        var normalisedCurrent = SiPrefixes.NormaliseValue(motorCurrent, SiUnits.Current);
        var radPerSec = ConvertKvtoRadPerSec(kvRating.Value);

        double torque = (1 / radPerSec) * normalisedCurrent;

        return new MeasureandQuantity(Math.Round(torque,4), SiUnits.NewtonMeter.Name);
    }

    public static double ConvertKvtoRadPerSec(double rpm)
    {
        return (rpm / 60) * 2 * (Math.PI);
    }
  
    public static MeasureandQuantity CalculateTorque(MeasureandQuantity motorKvRatio, MeasureandQuantity voltage, MeasureandQuantity motorCurrent, double motorEfficiency)
    {
        double normalisedVoltage = SiPrefixes.NormaliseValue(voltage, SiUnits.Voltage);
        double normalisedCurrent = SiPrefixes.NormaliseValue(motorCurrent, SiUnits.Current);
        var rpm = CalculateRpm(motorKvRatio.Value, voltage);
        double radPerSec = ConvertKvtoRadPerSec(rpm.Value);

        var torque = (normalisedCurrent * normalisedVoltage * motorEfficiency) / radPerSec;

        return new MeasureandQuantity(Math.Round(torque, 4), SiUnits.NewtonMeter.Name);
    }
}
