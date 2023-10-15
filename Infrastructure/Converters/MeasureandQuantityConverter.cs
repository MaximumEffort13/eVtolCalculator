using Domain.Entities.DetailedDesign.Battery;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Converters;

internal class MeasureandQuantityConverter : ValueConverter<MeasureandQuantity, string>
{
    private static readonly char[] separator = new char[] { ' ' };

    public MeasureandQuantityConverter() : base(t => $"{t.Value} {t.Unit}",
        value => new MeasureandQuantity(Convert.ToDouble(value.Split(separator)[0]), value.Split(separator)[1]))
    {
        
    }
}