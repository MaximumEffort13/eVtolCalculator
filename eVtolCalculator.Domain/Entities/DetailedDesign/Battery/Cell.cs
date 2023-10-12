using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class Cell : Entity
{
    public Cell(Guid id, MeasureandQuantity voltage, MeasureandQuantity capacity, MeasureandQuantity current, MeasureandQuantity weight) : base(id)
    {
        Voltage = voltage;
        Capacity = capacity;
        Current = current;
        Weight = weight;
    }

    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public bool Enabled { get; private set; }
}
