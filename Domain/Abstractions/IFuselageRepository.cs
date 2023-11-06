using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IFuselageRepository
{
    void Create(FuselageEntity fuselage);
    Task<List<FuselageEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<FuselageEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}