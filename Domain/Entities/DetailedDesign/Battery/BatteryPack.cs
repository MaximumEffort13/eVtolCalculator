using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryPack : Entity
{
    public Guid ModuleId { get; private set; }
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public MeasureandQuantity Power { get; private set; }
    public string Name { get; private set; }
    public BatteryModule Module { get; private set; }
    public int NumberOfModulesConnectedInSeries { get; private set; }
    public int NumberOfModulesConnectedInParallel { get; private set; }
    public MeasureandQuantity MiscellaneousWeight { get; private set; }
    public MeasureandQuantity SpecificEnergy { get; private set; }
    public MeasureandQuantity Weight { get; private set; } // Total Weigth should not exceed 50% of total weight

    public BatteryPack(
        Guid id,
        string name,
        BatteryModule module,
        int numberOfModulesConnectedInSeries,
        int numberOfModulesConnectedInParallel,
        double miscellaneousWeight) : base(id)
    {
        Name = name;
        Module = module;
        ModuleId = module.Id;
        NumberOfModulesConnectedInSeries = numberOfModulesConnectedInSeries;
        NumberOfModulesConnectedInParallel = numberOfModulesConnectedInParallel;
        MiscellaneousWeight = new MeasureandQuantity(miscellaneousWeight, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        Voltage = CalculateVoltage();
        Current = CalculateCurrent();
        Capacity = CalculateCapacity();
        Weight = CalculateWeight();
        SpecificEnergy = CalculateSpecificEnergy();
        Power = CalculatePower();
    }

    private MeasureandQuantity CalculateSpecificEnergy()
    {
        return new MeasureandQuantity(Capacity.Value / Weight.Value, $"{SiUnits.WattHour.Name}/{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    private MeasureandQuantity CalculateCurrent()
    {
        return  new MeasureandQuantity(Module.Current.Value * NumberOfModulesConnectedInParallel, SiUnits.Current.Name);
    }

    private MeasureandQuantity CalculateVoltage()
    {
        return new MeasureandQuantity(Module.Voltage.Value * NumberOfModulesConnectedInSeries, SiUnits.Voltage.Name);
    }

    private MeasureandQuantity CalculateCapacity()
    {
        return new MeasureandQuantity(Module.Capacity.Value * NumberOfModulesConnectedInParallel, SiPrefixes.Kilo.Name + SiUnits.WattHour.Name);
    }

    private MeasureandQuantity CalculateWeight()
    {
        var packWeight = Module.Weight.Value * NumberOfModulesConnectedInSeries + MiscellaneousWeight.Value;
        var prefix = Module.Weight.Unit;

        if (Module.Weight.Unit.Equals(SiUnits.Mass.Name) && packWeight > 1000)
        {
            packWeight /= 1000;
            prefix = SiPrefixes.Kilo.Name + Module.Weight.Unit;
        }

        return new MeasureandQuantity(packWeight, prefix);
    }

    private MeasureandQuantity CalculatePower()
    {
        return new MeasureandQuantity(Voltage.Value * Current.Value, SiUnits.Power.Name);
    }
}
