using Ardalis.SmartEnum;

namespace Domain.Enums;

public class SiPrefixes : SmartEnum<SiPrefixes, double>
{
    public static readonly SiPrefixes Pico = new("p", 0.0000000000001);
    public static readonly SiPrefixes Nano = new("n", 0.0000000001);
    public static readonly SiPrefixes Micro = new("u", 0.0000001);
    public static readonly SiPrefixes Milli = new("m", 0.0001);
    public static readonly SiPrefixes Kilo = new("k", 1000);
    public static readonly SiPrefixes Mega = new("M", 1000000);
    public static readonly SiPrefixes Giga = new("G", 1000000000);
    public static readonly SiPrefixes Tera = new("T", 1000000000000);

    public SiPrefixes(string prefix, double prefixMulitplier)
        : base(prefix, prefixMulitplier)
    {
    }
}