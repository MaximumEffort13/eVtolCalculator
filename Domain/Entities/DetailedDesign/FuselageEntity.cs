using Domain.Primitives;

namespace Domain.Entities.DetailedDesign;

public sealed class FuselageEntity : Entity
{
    public FuselageEntity(Guid id, Guid userId, MeasureandQuantity weight) : base(id)
    {
        UserId = userId;
        Weight = weight;
    }

    public Guid UserId { get; private set; }
    public MeasureandQuantity Weight { get; private set; }
}
