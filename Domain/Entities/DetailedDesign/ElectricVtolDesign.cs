using Domain.ConstantValues;
using Domain.Entities.ConceptDesign;
using Domain.Entities.DetailedDesign.Battery;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class ElectricVtolDesign : Entity
{
    public ElectricVtolDesign(
        Guid id,
        string name,
        Guid batteryPackId,
        Guid inverterId,
        Guid motorId,
        Guid bladeId,
        Guid fuselageId,
        Guid missionParameterId,
        int motorQuantity,
        int bladePerMotorQuantity) : base(id)
    {
        Name = name;

        BatteryPackId = batteryPackId;
        BladeId = bladeId;
        InverterId = inverterId;
        MotorId = motorId;
        FuselageId = fuselageId;

        MotorQuantity = motorQuantity;
        BladePerMotorQuantity = bladePerMotorQuantity;

        MissionParameterId = missionParameterId;
    }

    public Guid BatteryPackId { get; private set; }
    public Guid MotorId { get; private set; }
    public Guid InverterId { get; private set; }
    public Guid BladeId { get; private set; }
    public Guid FuselageId { get; private set; }
    public Guid MissionParameterId { get; private set; }
    public string Name { get; private set; }
    public int MotorQuantity { get; private set; }
    public int BladePerMotorQuantity { get; private set; }
    public TimeSpan FlightTimeInMinutes { get; private set; }
    public MeasureandQuantity PayloadWeight { get; private set; }

    public MeasureandQuantity ThrustArea { get; set; }
    public MeasureandQuantity LiftOffWeight { get; set; }
    public MeasureandQuantity DiscLoading { get; set; }
    public MeasureandQuantity PowerLoading { get; set; }
    public MeasureandQuantity Thrust { get; set; }
}
