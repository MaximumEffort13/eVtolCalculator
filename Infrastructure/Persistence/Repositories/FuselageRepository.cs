using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class FuselageRepository : IFuselageRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public FuselageRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(FuselageEntity fuselage)
    {
        _appDbContext.Fuselages.Add(fuselage);
    }

    public async Task<FuselageEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.SingleAsync(mod => mod.Id == id, cancellationToken);
    }

    public async Task<List<FuselageEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.ToListAsync(cancellationToken);
    }
}
