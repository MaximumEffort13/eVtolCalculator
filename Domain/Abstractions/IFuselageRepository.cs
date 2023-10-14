using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IFuselageRepository
{
    void Create(Fuselage fuselage);
    Task<List<Fuselage>> GetAllAsync(CancellationToken cancellationToken);
    Task<Fuselage> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}