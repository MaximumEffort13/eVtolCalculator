using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class Inverter : Entity
{
    public Inverter(Guid id, MeasureandQuantity weight, MeasureandQuantity voltageRating, MeasureandQuantity currentRating) : base(id)
    {
        Weight = weight;
        VoltageRating = voltageRating;
        CurrentRating = currentRating;
        PowerToWeightRatio = CalculatePowerToWeightRatio();
    }

    public MeasureandQuantity VoltageRating { get; private set; }
    public MeasureandQuantity CurrentRating { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity PowerToWeightRatio { get; private set; }

    private MeasureandQuantity CalculatePowerToWeightRatio()
    {
        return new MeasureandQuantity(
            (VoltageRating.Value * CurrentRating.Value) / Weight.Value, 
            $"{SiPrefixes.Kilo.Name}{SiUnits.Power.Name}/{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }
}
