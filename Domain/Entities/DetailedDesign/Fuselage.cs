using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class Fuselage : Entity
{
    public Fuselage(Guid id, MeasureandQuantity weight) : base(id)
    {
        Weight = weight;
    }

    public MeasureandQuantity Weight { get; private set; }
}
