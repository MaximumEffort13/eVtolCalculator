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


    public static MeasureandQuantity ConvertStringToMeasureandQuantity(string valueToConvert)
    {
        char separator = ' ';
        var arrayOfStrng = valueToConvert.Split(separator);
        double value = Convert.ToDouble(arrayOfStrng[0]);
        string unit = arrayOfStrng[1];

        return new MeasureandQuantity(value, unit);
    }

    public static string ToString(MeasureandQuantity quantity)
    {
        return $"{quantity.Value} {quantity.Unit}";
    }
}
