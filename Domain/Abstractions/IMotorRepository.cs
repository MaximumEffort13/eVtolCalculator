using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IMotorRepository
{
    void Create(Motor motor);
    Task<List<Motor>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<Motor> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<Motor> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken);
}