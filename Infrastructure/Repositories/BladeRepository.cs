using Domain.Entities.DetailedDesign;
using Domain.Abstractions;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BladeRepository : IBladeRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public BladeRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(Blade blade)
    {
        _appDbContext.Blades.Add(blade);
    }

    public async Task<Blade> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.SingleAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<List<Blade>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.ToListAsync(cancellationToken);
    }

    public async Task<Blade> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _appDbContext.Blades.SingleAsync(b => b.Name == name, cancellationToken);
    }
}
