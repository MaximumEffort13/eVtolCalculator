using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Converters
{
    internal class MeasurementQuantityConverter : ValueConverter<MeasureandQuantity, string>
    {
        public MeasurementQuantityConverter() : base(t => $"{t.Value} {t.Unit}",
            value => new MeasureandQuantity(Convert.ToDouble(value.Split(new char[] { ' ' })[0]), value.Split(new char[] { ' ' })[1]))
        {
            
        }
    }
}
