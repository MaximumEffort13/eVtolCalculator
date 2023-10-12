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
        BatteryPack battery,
        Inverter inverter,
        Motor motor,
        Blade blade,
        Fuselage fuselage,
        int motorQuantity,
        int bladePerMotorQuantity,
        MeasureandQuantity payloadWeight,
        TimeSpan flightTimeInMinutes) : base(id)
    {
        Battery = battery;
        Inverter = inverter;
        Motor = motor;
        Blade = blade;
        Fuselage = fuselage;
        MotorQuantity = motorQuantity;
        BladePerMotorQuantity = bladePerMotorQuantity;
        PayloadWeight = payloadWeight;
        FlightTimeInMinutes = flightTimeInMinutes;

        LiftOffWeight = CalculateLiftOffWeight();

        MissionParameters = new MissionParameterEstimates(Guid.NewGuid(), LiftOffWeight, payloadWeight, FlightTimeInMinutes);

        ThrustArea = CalculateThrustArea();
        DiscLoading = CalculateDiscLoading();
        PowerLoading = CalculatePowerLoading();
        Thrust = CalculateThrustRequirement();
    }

    public MissionParameterEstimates MissionParameters { get; private set; }
    public BatteryPack Battery{ get; private set; } //Should be within 20% tolerance of Estimated
    public Inverter Inverter { get; private set; }
    public Motor Motor { get; private set; }
    public Blade Blade{ get; private set; }
    public Fuselage Fuselage { get; private set; }
    public int MotorQuantity { get; private set; }
    public int BladePerMotorQuantity { get; private set; }
    public MeasureandQuantity ThrustArea { get; private set; }
    public MeasureandQuantity LiftOffWeight { get; private set; }
    public MeasureandQuantity PayloadWeight { get; private set; }
    public TimeSpan FlightTimeInMinutes { get; private set; }
    public MeasureandQuantity DiscLoading { get; private set; }
    public MeasureandQuantity PowerLoading { get; private set; }
    public MeasureandQuantity Thrust { get; private set; }


    private MeasureandQuantity CalculateThrustArea()
    {
        return new MeasureandQuantity(Math.PI * Math.Pow(Blade.Length.Value, 2) * MotorQuantity * BladePerMotorQuantity, SiUnits.Meter.Name);
    }

    private MeasureandQuantity CalculateLiftOffWeight()
    {
        return new MeasureandQuantity(
                PayloadWeight.Value +
                Battery.Weight.Value +
                (Motor.Weight.Value * MotorQuantity) +
                (Inverter.Weight.Value * MotorQuantity) +
                (Blade.Weight.Value * MotorQuantity * BladePerMotorQuantity) +
                Fuselage.Weight.Value,
                SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
    }

    private MeasureandQuantity CalculateThrustRequirement()
    {
        double motorFigureOfMertig = 0.75;
        double power = MissionParameters.EstimatedPowerRequirement.Value;
        double freeStreamAirDensity = PredefinedConstantValues.airDensityFactor;

        double phase2 = Math.Pow(motorFigureOfMertig * power * Math.Sqrt(2), 2);
        double t3 = phase2 * ThrustArea.Value * freeStreamAirDensity;
        double thrust = Math.Cbrt(t3);

        return new MeasureandQuantity(thrust, $"{SiUnits.NewtonMeter.Name}/{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
    }

    private MeasureandQuantity CalculateDiscLoading()
    {
        return new MeasureandQuantity(LiftOffWeight.Value / ThrustArea.Value, $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}{SiUnits.Meter}");
    }

    private MeasureandQuantity CalculatePowerLoading()
    {
        return new MeasureandQuantity(LiftOffWeight.Value / MissionParameters.EstimatedHorsepowerRequiredForHover.Value, $"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}/{SiUnits.Horsepower}");
    }
}
