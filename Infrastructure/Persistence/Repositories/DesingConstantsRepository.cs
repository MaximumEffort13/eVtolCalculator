using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public sealed class DesignConstantsRepository(ApplicationDbContext appDbContext) : IDesignConstantsRepository
{
    private readonly ApplicationDbContext _appDbContext = appDbContext;

    public void Create(DesignConstantsEntity designConst)
    {
        _appDbContext.DesignConstants.Add(designConst);
    }

    public async Task<DesignConstantsEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.DesignConstants.SingleAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<List<DesignConstantsEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.DesignConstants.ToListAsync(cancellationToken);
    }

    public async Task<DesignConstantsEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _appDbContext.DesignConstants.SingleAsync(b => b.Name == name, cancellationToken);
    }

    public async Task<bool> IsNameUnique(string name)
    {
        return await _appDbContext.DesignConstants.AnyAsync(a => a.Name == name) == false;
    }
}
