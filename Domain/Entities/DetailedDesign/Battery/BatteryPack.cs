using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryPack : Entity
{
    public Guid ModuleId { get; private set; }
    public MeasureandQuantity Capacity { get; set; }
    public MeasureandQuantity Voltage { get; set; }
    public MeasureandQuantity Current { get; set; }
    public MeasureandQuantity Power { get; set; }
    public string Name { get; private set; }
    public int NumberOfModulesConnectedInSeries { get; private set; }
    public int NumberOfModulesConnectedInParallel { get; private set; }
    public MeasureandQuantity MiscellaneousWeight { get; set; }
    public MeasureandQuantity SpecificEnergy { get; set; }
    public MeasureandQuantity Weight { get; set; } // Total Weigth should not exceed 50% of total weight


    public BatteryPack(
        Guid id,
        string name,
        Guid moduleId,
        int numberOfModulesConnectedInSeries,
        int numberOfModulesConnectedInParallel,
        MeasureandQuantity miscellaneousWeight) : base(id)
    {
        Name = name;
        ModuleId = moduleId;
        NumberOfModulesConnectedInSeries = numberOfModulesConnectedInSeries;
        NumberOfModulesConnectedInParallel = numberOfModulesConnectedInParallel;
        MiscellaneousWeight = miscellaneousWeight;
    }
}
