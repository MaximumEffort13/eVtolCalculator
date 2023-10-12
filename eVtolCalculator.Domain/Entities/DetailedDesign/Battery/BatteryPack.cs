using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign.Battery;

public sealed class BatteryPack : Entity
{
    public MeasureandQuantity Capacity { get; private set; }
    public MeasureandQuantity Voltage { get; private set; }
    public MeasureandQuantity Current { get; private set; }
    public BatteryModule Module { get; private set; }
    public int ModuleCount { get; private set; }
    public ConnectionOrientation ModuleOrientation { get; private set; }
    public double MiscellaneousWeight { get; private set; }
    public MeasureandQuantity SpecificEnergy { get; private set; }
    public MeasureandQuantity Weight { get; private set; } // Total Weigth should not exceed 50% of total weight

    public BatteryPack(Guid id, BatteryModule module, int moduleCount, ConnectionOrientation moduleOrientation, double miscellaneousWeight) : base(id)
    {
        Module = module;
        ModuleCount = moduleCount;
        ModuleOrientation = moduleOrientation;
        MiscellaneousWeight = miscellaneousWeight;
        Voltage = CalculateVoltage();
        Current = CalculateCurrent();
        Capacity = CalculateCapacity();
        Weight = CalculateWeight();
        SpecificEnergy = CalculateSpecificEnergy();
    }

    private MeasureandQuantity CalculateSpecificEnergy()
    {
        return new MeasureandQuantity(Capacity.Value / Weight.Value, $"{SiUnits.WattHour.Name}/{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    private MeasureandQuantity CalculateCurrent()
    {
        return ModuleOrientation switch { ConnectionOrientation.Parallel => new MeasureandQuantity(Module.Current.Value * ModuleCount, SiUnits.Current.Name), _ => Module.Current };
    }

    private MeasureandQuantity CalculateVoltage()
    {
        return ModuleOrientation switch { ConnectionOrientation.Series => new MeasureandQuantity(Module.Voltage.Value * ModuleCount, SiUnits.Voltage.Name), _ => Module.Voltage };
    }

    private MeasureandQuantity CalculateCapacity()
    {
        return ModuleOrientation switch { ConnectionOrientation.Parallel => new MeasureandQuantity(Module.Capacity.Value * ModuleCount, SiPrefixes.Kilo.Name + SiUnits.WattHour.Name), _ => Module.Capacity };
    }

    private MeasureandQuantity CalculateWeight()
    {
        var packWeight = Module.Weight.Value * ModuleCount + MiscellaneousWeight;
        var prefix = Module.Weight.Unit;

        if (Module.Weight.Unit.Equals(SiUnits.Mass.Name) && packWeight > 1000)
        {
            packWeight /= 1000;
            prefix = SiPrefixes.Kilo.Name + Module.Weight.Unit;
        }

        return new MeasureandQuantity(packWeight, prefix);
    }
}
