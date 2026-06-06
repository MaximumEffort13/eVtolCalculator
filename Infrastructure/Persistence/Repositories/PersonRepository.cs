using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;
using Domain.Entities.AuthenticationModels;
using Domain.Abstractions;

namespace Infrastructure.Persistence.Repositories;

public sealed class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public PersonRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(PersonEntity person)
    {
        _appDbContext.People.Add(person);
    }

    public async Task<PersonEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.People.SingleAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PersonEntity> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.People.SingleAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task<List<PersonEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.People.ToListAsync(cancellationToken);
    }

    public async Task<PersonEntity> GetByNameAsync(string firstName, CancellationToken cancellationToken)
    {
        return await _appDbContext.People.SingleAsync(m => m.FirstName == firstName, cancellationToken);
    }
}
