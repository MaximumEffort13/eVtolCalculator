using Domain.Enums;

namespace Domain.Primitives;

public sealed class MeasureandQuantity
{
    public string Unit { get; set; }
    public double Value { get; set; }

    public MeasureandQuantity(double value, string unit)
    {
        Value = value;
        Unit = unit;
    }
}
