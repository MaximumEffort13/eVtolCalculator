using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IInvererRepository
{
    void Create(InverterEntity inverter);
    Task<List<InverterEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<InverterEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<InverterEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
}