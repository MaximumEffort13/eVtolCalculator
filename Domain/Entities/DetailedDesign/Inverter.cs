using Domain.EntityCalculations;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class Inverter : Entity
{
    public Inverter(Guid id, string name, MeasureandQuantity weight, MeasureandQuantity voltageRating, MeasureandQuantity currentRating) : base(id)
    {
        Name = name;
        Weight = weight;
        VoltageRating = voltageRating;
        CurrentRating = currentRating;
        PowerToWeightRatio = ElectricCalculations.CalculatePowerToWeightRatio(voltageRating, currentRating, weight);
    }

    public string Name { get; private set; }
    public MeasureandQuantity VoltageRating { get; private set; }
    public MeasureandQuantity CurrentRating { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity PowerToWeightRatio { get; private set; }
}
