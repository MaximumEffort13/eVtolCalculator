using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class FuselageRepository : IFuselageRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public FuselageRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(Fuselage fuselage)
    {
        _appDbContext.Fuselages.Add(fuselage);
    }

    public async Task<Fuselage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.SingleAsync(mod => mod.Id == id, cancellationToken);
    }

    public async Task<List<Fuselage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.ToListAsync(cancellationToken);
    }
}
