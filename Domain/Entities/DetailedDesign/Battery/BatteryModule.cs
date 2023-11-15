using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryModule : Entity
{
    public BatteryModule(
        Guid id,
        Guid cellId,
        int numberOfCellsConnectedInSeries,
        int numberOfCellsConnectedInParallel) : base(id)
    {
        CellId = cellId;
        NumberOfCellsConnectedInSeries = numberOfCellsConnectedInSeries;
        NumberOfCellsConnectedInParallel = numberOfCellsConnectedInParallel;
    }

    public Guid CellId { get; private set; }
    public int NumberOfCellsConnectedInSeries { get; private set; }
    public int NumberOfCellsConnectedInParallel { get; private set; }
    public MeasureandQuantity Capacity { get; set; }
    public MeasureandQuantity Voltage { get; set; }
    public MeasureandQuantity Energy { get; set; }
    public MeasureandQuantity Mass { get; set; }

}
