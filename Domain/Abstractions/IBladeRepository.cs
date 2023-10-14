using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IBladeRepository
{
    void Create(Blade blade);
    Task<List<Blade>> GetAllAsync(CancellationToken cancellationToken);
    Task<Blade> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}