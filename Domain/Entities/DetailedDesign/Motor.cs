using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class Motor : Entity
{
    public Motor(Guid id, string name, MeasureandQuantity voltageRating, MeasureandQuantity currentRating, MeasureandQuantity weight, MeasureandQuantity kv) : base(id)
    {
        Name = name;
        VoltageRating = voltageRating;
        CurrentRating = currentRating;
        Weight = weight;
        Kv = kv;
        PowerToWeightRatio = CalculatePowerToWeightRatio();
        Rpm = CalculateRpm();
    }


    public string Name { get; private set; }
    /// <summary>
    /// Voltage rating for the motor. Is used to calculate the maximum RPM's
    /// </summary>
    public MeasureandQuantity VoltageRating { get; private set; }

    /// <summary>
    /// Current rating of the motor. Is used to determine Inverter max ouput current.
    /// </summary>
    public MeasureandQuantity CurrentRating { get; private set; }

    /// <summary>
    /// Weight of the motor.
    /// </summary>
    public MeasureandQuantity Weight { get; private set; }


    /// <summary>
    /// Revolutions per minute
    /// </summary>
    public double Rpm { get; private set; }

    /// <summary>
    /// Kv determines the amount of rpm per volt.
    /// </summary>
    public MeasureandQuantity Kv { get; set; }

    /// <summary>
    /// The amount of power generated per unit of weight that the motor weighs.
    /// </summary>
    public MeasureandQuantity PowerToWeightRatio { get; private set; }


    private MeasureandQuantity CalculatePowerToWeightRatio()
    {
        return new MeasureandQuantity(
            (VoltageRating.Value * CurrentRating.Value) / Weight.Value,
            $"{SiPrefixes.Kilo.Name}{SiUnits.Power.Name}/{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    private double CalculateRpm()
    {
        return Kv.Value * VoltageRating.Value;
    }
}
