using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class Cell : Entity
{
    public Cell(Guid id, string name, MeasureandQuantity voltage, MeasureandQuantity capacity, MeasureandQuantity current, MeasureandQuantity weight) : base(id)
    {
        Name = name;
        Voltage = voltage;
        Capacity = capacity;
        Current = current;
        Weight = weight;
    }

    public string Name { get; private set; }

    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
}
