using Domain.ConstantValues;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public class AerodynamicCalculations
{
    /// <summary>
    /// Calculates the thrust are of the propeller blades used.
    /// </summary>
    /// <param name="lengthOfBlade">The lenfth of the blade generai</param>
    /// <param name="MotorQuantity"></param>
    /// <returns></returns>
    public static MeasureandQuantity CalculateThrustArea(MeasureandQuantity lengthOfBlade, int MotorQuantity)
    {
        var normalisedLength = SiPrefixes.NormaliseValue(lengthOfBlade, SiUnits.Meter);

        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.PI * Math.Pow(normalisedLength / 2, 2) * MotorQuantity, SiUnits.Meter);
    }

    public static MeasureandQuantity CalculateThrustRequirement(MeasureandQuantity powerRequirement, MeasureandQuantity thrustArea)
    {
        double motorFigureOfMertig = 0.75;
        double normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);
        double freeStreamAirDensity = PredefinedConstantValues.airDensityFactor;
        double normalisedThrustarea = SiPrefixes.NormaliseValue(thrustArea, SiUnits.Meter);

        double phase2 = Math.Pow(motorFigureOfMertig * normalisedPower * Math.Sqrt(2), 2);
        double t3 = phase2 * normalisedThrustarea * freeStreamAirDensity;
        double thrust = Math.Cbrt(t3);

        return new MeasureandQuantity(thrust, $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    public static MeasureandQuantity CalculateDiscLoading(MeasureandQuantity liftOffWeight, MeasureandQuantity thrustArea)
    {
        var normalisedWeight = SiPrefixes.NormaliseValue(liftOffWeight, SiUnits.Mass);
        var normalisedThrustArea = SiPrefixes.NormaliseValue(thrustArea, SiUnits.Meter);

        return new MeasureandQuantity(normalisedWeight * SiPrefixes.Kilo.Value / normalisedThrustArea, $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}/{SiUnits.Meter}^2");
    }

    public static MeasureandQuantity CalculatePowerLoading(MeasureandQuantity liftOffWeight, MeasureandQuantity horsepower)
    {
        var normalisedWeight = SiPrefixes.NormaliseValue(liftOffWeight, SiUnits.Mass);
        var normalisedHorsepower = SiPrefixes.NormaliseValue(horsepower, SiUnits.Horsepower);

        return new MeasureandQuantity(normalisedWeight * SiPrefixes.Kilo.Value / normalisedHorsepower, $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}/{SiUnits.Horsepower}");
    }
}
