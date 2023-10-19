using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class FuselageEntity : Entity
{
    public FuselageEntity(Guid id, MeasureandQuantity weight) : base(id)
    {
        Weight = weight;
    }

    public MeasureandQuantity Weight { get; private set; }
}
