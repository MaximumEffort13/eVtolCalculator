using Ardalis.SmartEnum;
using Domain.Primitives;

namespace Domain.Enums;

public class SiPrefixes : SmartEnum<SiPrefixes, double>
{
    public static readonly SiPrefixes Pico = new("p", 0.000000000001);
    public static readonly SiPrefixes Nano = new("n", 0.000000001);
    public static readonly SiPrefixes Micro = new("u", 0.000001);
    public static readonly SiPrefixes Milli = new("m", 0.001);
    public static readonly SiPrefixes None = new("None", 1);
    public static readonly SiPrefixes Kilo = new("k", 1000);
    public static readonly SiPrefixes Mega = new("M", 1000000);
    public static readonly SiPrefixes Giga = new("G", 1000000000);
    public static readonly SiPrefixes Tera = new("T", 1000000000000);

    public SiPrefixes(string prefix, double prefixMulitplier)
        : base(prefix, prefixMulitplier)
    {
    }

    public static SiPrefixes FindPrefixFromValue(double value)
    {
        return value switch
        {
            0.001 => Milli,
            0.000001 => Micro,
            0.000000001 => Nano,
            0.000000000001 => Pico,
            1000 => Kilo,
            1000000 => Mega,
            1000000000 => Giga,
            1000000000000 => Tera,
            _ => None,
        };
    }


    public static SiPrefixes FindPrefixFromName(char value)
    {
        return value switch
        {
            'm' => Milli,
            'u' => Micro,
            'n' => Nano,
            'p' => Pico,
            'k' => Kilo,
            'M' => Mega,
            'G' => Giga,
            'T' => Tera,
            _ => None,
        };
    }

    public static MeasureandQuantity ScaleNormalisedValueToAppropriateUnit(double value, SiUnits baseUnit)
    {
        double scaledValue = value;

        SiPrefixes prefix = SiPrefixes.None;

        if (value > SiPrefixes.Kilo.Value)
        {
            prefix = SiPrefixes.Kilo;
            scaledValue = value / SiPrefixes.Kilo.Value;
        }
        else if (value < 0)
        {
            prefix = SiPrefixes.Milli;
            scaledValue = value / SiPrefixes.Milli.Value;
        }

        var prefixName = prefix == SiPrefixes.None ? "" : prefix.Name;

        return new MeasureandQuantity(scaledValue, prefixName + baseUnit.Name);
    }

    public static double NormaliseValue(MeasureandQuantity valueToNormalise, SiUnits baseUnit)
    {
        double normalisedValue = valueToNormalise.Value;

        if (valueToNormalise.Unit != null && valueToNormalise.Unit.StartsWith(baseUnit.Name) == false)
        {
            var prefix = SiPrefixes.FindPrefixFromName(valueToNormalise.Unit[0]);

            normalisedValue = valueToNormalise.Value * prefix.Value;
        }

        if (valueToNormalise.Unit == "mm")
        {
            var prefix = SiPrefixes.FindPrefixFromName(valueToNormalise.Unit[0]);

            normalisedValue = valueToNormalise.Value * prefix.Value;
        }

        return normalisedValue;
    }
}