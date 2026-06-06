using Domain.Entities.AuthenticationModels;

namespace Domain.Abstractions;

public interface IPersonRepository
{
    void Create(PersonEntity person);
    Task<List<PersonEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<PersonEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PersonEntity> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<PersonEntity> GetByNameAsync(string firstName, CancellationToken cancellationToken);
}