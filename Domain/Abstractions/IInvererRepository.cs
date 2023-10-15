using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IInvererRepository
{
    void Create(Inverter inverter);
    Task<List<Inverter>> GetAllAsync(CancellationToken cancellationToken);
    Task<Inverter> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Inverter> GetByNameAsync(string name, CancellationToken cancellationToken);
}