using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IBladeRepository
{
    void Create(BladeEntity blade);
    Task<List<BladeEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<BladeEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<BladeEntity> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken);
}