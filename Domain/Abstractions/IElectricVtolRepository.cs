using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IElectricVtolRepository
{
    void Create(ElectricVtolDesign electricVtolDesign);
    Task<List<ElectricVtolDesign>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ElectricVtolDesign> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<ElectricVtolDesign> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken);
}