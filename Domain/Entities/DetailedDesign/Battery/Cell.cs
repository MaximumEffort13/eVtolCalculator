using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class Cell : Entity
{
    public Cell(Guid id, string name, MeasureandQuantity voltage, MeasureandQuantity capacity, MeasureandQuantity energy, MeasureandQuantity cellMass) : base(id)
    {
        Name = name;
        Voltage = voltage;
        Capacity = capacity;
        Energy = energy;
        CellMass = cellMass;
    }

    public string Name { get; private set; }

    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Energy { get; private set; }
    public MeasureandQuantity CellMass { get; private set; }
}
