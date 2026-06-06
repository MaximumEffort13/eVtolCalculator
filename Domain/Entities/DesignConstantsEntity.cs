using Domain.Primitives;

namespace Domain.Entities;

public sealed class DesignConstantsEntity(Guid id, string name, double value) : Entity(id)
{
    public string Name { get; set; } = name;
    public double Value { get; set; } = value;
}
