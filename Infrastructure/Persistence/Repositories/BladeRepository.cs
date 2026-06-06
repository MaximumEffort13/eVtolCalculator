using Domain.Entities.DetailedDesign;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;

namespace Infrastructure.Persistence.Repositories;

public sealed class BladeRepository : IBladeRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public BladeRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(BladeEntity blade)
    {
        _appDbContext.Blades.Add(blade);
    }

    public async Task<BladeEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.SingleAsync(b => b.Id == id && b.UserId == userId, cancellationToken);
    }

    public async Task<List<BladeEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.Where(b => b.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<BladeEntity> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.SingleAsync(b => b.Name == name && b.UserId == userId, cancellationToken);
    }
}
