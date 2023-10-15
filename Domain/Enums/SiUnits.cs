using Ardalis.SmartEnum;

namespace Domain.Enums;

public class SiUnits : SmartEnum<SiUnits, double>
{
    public static readonly SiUnits Mass = new("g", 1);
    public static readonly SiUnits Meter = new("m", 1);
    public static readonly SiUnits Newton = new("N", 1);
    public static readonly SiUnits NewtonMeter = new("Nm", 1);
    public static readonly SiUnits Voltage = new("V", 1);
    public static readonly SiUnits Current = new("A", 1);
    public static readonly SiUnits Watt = new("W", 1);
    public static readonly SiUnits WattHour = new("Wh", 1);
    public static readonly SiUnits Horsepower = new("hp", 1);
    public static readonly SiUnits Degress = new("º", 1);
    public static readonly SiUnits Rpm = new("rpm", 1);

    public SiUnits(string unit, double valueMultiplier)
        : base(unit, valueMultiplier)
    {
    }
}