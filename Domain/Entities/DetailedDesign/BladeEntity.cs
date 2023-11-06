using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class BladeEntity : Entity
{
    public BladeEntity(
        Guid id,
        Guid userId,
        string name,
        MeasureandQuantity length,
        MeasureandQuantity width,
        MeasureandQuantity thickness,
        MeasureandQuantity weight,
        MeasureandQuantity angleOfAttack) : base(id)
    {
        UserId = userId;
        Name = name;
        Length = length;
        Width = width;
        Thickness = thickness;
        Weight = weight;
        AngleOfAttack = angleOfAttack;
    }

    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public MeasureandQuantity Length { get; private set; }
    public MeasureandQuantity Width { get; private set; }
    public MeasureandQuantity Thickness { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
    public MeasureandQuantity AngleOfAttack { get; private set; }
}
