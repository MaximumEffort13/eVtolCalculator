using Domain.Entities;

namespace Domain.Abstractions;

public interface IDesignConstantsRepository
{
    void Create(DesignConstantsEntity designConst);
    Task<List<DesignConstantsEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<DesignConstantsEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<DesignConstantsEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> IsNameUnique(string name);
}