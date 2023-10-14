using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryModule : Entity
{
    public BatteryModule(Guid id, Cell cell, int numberOfCellsConnectedInSeries, int numberOfCellsConnectedInParallel) : base(id)
    {
        Cell = cell;
        CellId = cell.Id;
        NumberOfCellsConnectedInSeries = numberOfCellsConnectedInSeries;
        NumberOfCellsConnectedInParallel = numberOfCellsConnectedInParallel;
        Voltage = CalculateVoltage();
        Current = CalculateCurrent();
        Capacity = CalculateCapacity();
        Weight = CalculateWeight();
        Power = CalculatePower();
    }

    public Cell Cell { get; private set; }
    public Guid CellId { get; private set; }
    public int NumberOfCellsConnectedInSeries { get; private set; }
    public int NumberOfCellsConnectedInParallel { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity Power { get; private set; }


    private MeasureandQuantity CalculateVoltage()
    {
        return new MeasureandQuantity(Cell.Voltage.Value * NumberOfCellsConnectedInSeries, SiUnits.Voltage.Name);
    }

    private MeasureandQuantity CalculateCurrent()
    {
        return new MeasureandQuantity(Cell.Current.Value * NumberOfCellsConnectedInParallel, SiUnits.Current.Name);
    }

    private MeasureandQuantity CalculateCapacity()
    {
        return new MeasureandQuantity(Cell.Capacity.Value * NumberOfCellsConnectedInParallel, SiUnits.WattHour.Name);
    }

    private MeasureandQuantity CalculateWeight()
    {
        var moduleWeight = Cell.Weight.Value * (NumberOfCellsConnectedInSeries + NumberOfCellsConnectedInParallel);
        var unitPrefix = string.Empty;

        if (moduleWeight > 1000)
        {
            moduleWeight /= 1000;
            unitPrefix = SiPrefixes.Kilo.Name;
        }

        return new MeasureandQuantity(moduleWeight, unitPrefix + SiUnits.Mass.Name);
    }


    private MeasureandQuantity? CalculatePower()
    {
        return new MeasureandQuantity(Voltage.Value * Current.Value, SiUnits.Power.Name);
    }
}
