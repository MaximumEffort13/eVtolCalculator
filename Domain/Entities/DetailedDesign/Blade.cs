using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class Blade : Entity
{
    public Blade(
        Guid id,
        string name,
        MeasureandQuantity length,
        MeasureandQuantity width,
        MeasureandQuantity thickness,
        MeasureandQuantity weight,
        MeasureandQuantity angleOfAttack) : base(id)
    {
        Name = name;
        Length = length;
        Width = width;
        Thickness = thickness;
        Weight = weight;
        AngleOfAttack = angleOfAttack;
    }

    public string Name { get; private set; }
    public MeasureandQuantity Length { get; private set; }
    public MeasureandQuantity Width { get; private set; }
    public MeasureandQuantity Thickness { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity AngleOfAttack { get; private set; }
}
