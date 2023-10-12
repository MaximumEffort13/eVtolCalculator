using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryModule : Entity
{
    public BatteryModule(Guid id, Cell cell, int numberOfCells, ConnectionOrientation orientation) : base(id)
    {
        Cell = cell;
        NumberOfCells = numberOfCells;
        CellOrientation = orientation;
        Voltage = CalculateVoltage();
        Current = CalculateCurrent();
        Capacity = CalculateCapacity();
        Weight = CalculateWeight();
    }

    public Cell Cell { get; private set; }
    public int NumberOfCells { get; private set; }
    public ConnectionOrientation CellOrientation { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public MeasureandQuantity Weight { get; private set; }

    private MeasureandQuantity CalculateVoltage()
    {
        return CellOrientation switch
        {
            ConnectionOrientation.Series => new MeasureandQuantity(Cell.Voltage.Value * NumberOfCells, SiUnits.Voltage.Name),
            _ => Cell.Voltage,
        };
    }

    private MeasureandQuantity CalculateCurrent()
    {
        return CellOrientation switch
        {
            ConnectionOrientation.Parallel => new MeasureandQuantity(Cell.Current.Value * NumberOfCells, SiUnits.Current.Name),
            _ => Cell.Current,
        };
    }

    private MeasureandQuantity CalculateCapacity()
    {
        return CellOrientation switch
        {
            ConnectionOrientation.Parallel => new MeasureandQuantity(Cell.Capacity.Value * NumberOfCells, SiUnits.WattHour.Name),
            _ => Cell.Capacity,
        };
    }

    private MeasureandQuantity CalculateWeight()
    {
        var moduleWeight = Cell.Weight.Value * NumberOfCells;
        var unitPrefix = string.Empty;

        if (moduleWeight > 1000)
        {
            moduleWeight /= 1000;
            unitPrefix = SiPrefixes.Kilo.Name;
        }

        return new MeasureandQuantity(moduleWeight, unitPrefix + SiUnits.Mass.Name);
    }
}
