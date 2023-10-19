using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IBladeRepository
{
    void Create(BladeEntity blade);
    Task<List<BladeEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<BladeEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BladeEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
}