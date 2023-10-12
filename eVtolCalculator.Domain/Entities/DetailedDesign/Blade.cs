using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DetailedDesign;

public sealed class Blade : Entity
{
    public Blade(
        Guid id,
        MeasureandQuantity length,
        MeasureandQuantity width,
        MeasureandQuantity thickness,
        MeasureandQuantity weight,
        MeasureandQuantity angleOfAttack) : base(id)
    {
        Length = length;
        Width = width;
        Thickness = thickness;
        Weight = weight;
        AngleOfAttack = angleOfAttack;
    }

    public MeasureandQuantity Length { get; private set; }
    public MeasureandQuantity Width { get; private set; }
    public MeasureandQuantity Thickness { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity AngleOfAttack { get; private set; }
}
