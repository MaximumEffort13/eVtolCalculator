using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class BladeEntity(
    Guid id,
    Guid userId,
    string name,
    MeasureandQuantity diameter,
    MeasureandQuantity width,
    MeasureandQuantity thickness,
    MeasureandQuantity weight,
    MeasureandQuantity angleOfAttack) : Entity(id)
{
    public Guid UserId { get; private set; } = userId;
    public string Name { get; private set; } = name;
    public MeasureandQuantity Diameter { get; private set; } = diameter;
    public MeasureandQuantity Width { get; private set; } = width;
    public MeasureandQuantity Thickness { get; private set; } = thickness;
    public MeasureandQuantity Weight { get; private set; } = weight;
    public MeasureandQuantity AngleOfAttack { get; private set; } = angleOfAttack;
}
