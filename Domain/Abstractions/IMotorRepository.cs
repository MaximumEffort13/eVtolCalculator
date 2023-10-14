using Domain.Entities.DetailedDesign;

namespace Domain.Abstractions;

public interface IMotorRepository
{
    void Create(Motor motor);
    Task<List<Motor>> GetAllAsync(CancellationToken cancellationToken);
    Task<Motor> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}