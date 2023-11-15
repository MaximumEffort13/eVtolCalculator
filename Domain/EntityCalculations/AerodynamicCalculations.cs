using Domain.ConstantValues;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.EntityCalculations;

public class AerodynamicCalculations
{
    /// <summary>
    /// Calculates the thrust are of the propeller blades used.
    /// </summary>
    /// <param name="lengthOfBlade">The length of the blade.</param>
    /// <param name="MotorQuantity"></param>
    /// <returns></returns>
    public static MeasureandQuantity CalculateThrustArea(MeasureandQuantity lengthOfBlade, int MotorQuantity, int bladePerMotorQuantity)
    {
        var normalisedLength = SiPrefixes.NormaliseValue(lengthOfBlade, SiUnits.Meter);
        var thrustArea = Math.PI * Math.Pow(normalisedLength / 2, 2) * MotorQuantity * bladePerMotorQuantity;
        return SiPrefixes.ScaleNormalisedValueToAppropriateUnit(Math.Round(thrustArea, 4), SiUnits.Meter);
    }

    public static MeasureandQuantity CalculateThrustRequirement(MeasureandQuantity powerRequirement, MeasureandQuantity thrustArea)
    {
        double motorFigureOfMertit = 0.75;
        double normalisedPower = SiPrefixes.NormaliseValue(powerRequirement, SiUnits.Watt);
        double freeStreamAirDensity = PredefinedConstantValues.airDensityFactor;
        double normalisedThrustarea = SiPrefixes.NormaliseValue(thrustArea, SiUnits.Meter);

        double phase2 = Math.Pow(motorFigureOfMertit * normalisedPower * Math.Sqrt(2), 2);
        double t3 = phase2 * normalisedThrustarea * freeStreamAirDensity;
        double thrust = Math.Cbrt(t3);

        return new MeasureandQuantity(Math.Round(thrust, 4), $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    public static MeasureandQuantity CalculateDiscLoading(MeasureandQuantity liftOffWeight, MeasureandQuantity thrustArea)
    {
        var normalisedWeight = SiPrefixes.NormaliseValue(liftOffWeight, SiUnits.Mass);
        var normalisedThrustArea = SiPrefixes.NormaliseValue(thrustArea, SiUnits.Meter);

        return new MeasureandQuantity(Math.Round(normalisedWeight / SiPrefixes.Kilo.Value / normalisedThrustArea, 4), $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}/{SiUnits.Meter}^2");
    }

    public static MeasureandQuantity CalculatePowerLoading(MeasureandQuantity liftOffWeight, MeasureandQuantity horsepower)
    {
        var normalisedWeight = SiPrefixes.NormaliseValue(liftOffWeight, SiUnits.Mass);
        var normalisedHorsepower = SiPrefixes.NormaliseValue(horsepower, SiUnits.Horsepower);

        return new MeasureandQuantity(Math.Round(normalisedWeight / SiPrefixes.Kilo.Value / normalisedHorsepower, 4), $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}/{SiUnits.Horsepower}");
    }
}
