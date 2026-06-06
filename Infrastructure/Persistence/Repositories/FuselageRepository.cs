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

    public async Task<FuselageEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.SingleAsync(mod => mod.Id == id && mod.UserId == userId, cancellationToken);
    }

    public async Task<List<FuselageEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Fuselages.Where(mod => mod.UserId == userId).ToListAsync(cancellationToken);
    }
}
