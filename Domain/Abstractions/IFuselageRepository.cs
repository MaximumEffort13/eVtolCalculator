using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IFuselageRepository
{
    void Create(FuselageEntity fuselage);
    Task<List<FuselageEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<FuselageEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}