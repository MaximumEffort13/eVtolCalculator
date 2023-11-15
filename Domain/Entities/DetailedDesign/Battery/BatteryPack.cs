using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryPack : Entity
{
    public Guid UserId { get; private set; }
    public Guid ModuleId { get; private set; }
    public MeasureandQuantity Capacity { get; set; }
    public MeasureandQuantity Voltage { get; set; }
    public MeasureandQuantity Energy { get; set; }
    public string Name { get; private set; }
    public int NumberOfModulesConnectedInSeries { get; private set; }
    public int NumberOfModulesConnectedInParallel { get; private set; }
    public MeasureandQuantity MiscellaneousWeight { get; set; }
    public MeasureandQuantity SpecificEnergy { get; set; }
    public MeasureandQuantity Mass { get; set; } // Total Weigth should not exceed 50% of total weight


    public BatteryPack(
        Guid id,
        Guid userId,
        string name,
        Guid moduleId,
        int numberOfModulesConnectedInSeries,
        int numberOfModulesConnectedInParallel,
        MeasureandQuantity miscellaneousWeight) : base(id)
    {
        UserId = userId;
        Name = name;
        ModuleId = moduleId;
        NumberOfModulesConnectedInSeries = numberOfModulesConnectedInSeries;
        NumberOfModulesConnectedInParallel = numberOfModulesConnectedInParallel;
        MiscellaneousWeight = miscellaneousWeight;
    }
}
