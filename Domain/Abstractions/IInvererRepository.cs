using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IInvererRepository
{
    void Create(InverterEntity inverter);
    Task<List<InverterEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<InverterEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<InverterEntity> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken);
}