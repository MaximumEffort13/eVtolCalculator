using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class BatteryPackRepository : IBatteryPackRepository
{
    private readonly ApplicationDbContext _appContext;

    public BatteryPackRepository(ApplicationDbContext appContext)
    {
        _appContext = appContext;
    }

    public void CreateBatteryPack(BatteryPack battery)
    {
        _appContext.BatteryPacks.Add(battery);
    }

    public async Task<BatteryPack> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appContext.BatteryPacks.SingleAsync(b => b.Id == id && b.UserId == userId, cancellationToken);
    }

    public async Task<BatteryPack> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await _appContext.BatteryPacks.SingleAsync(b => b.Name == name && b.UserId == userId, cancellationToken);
    }

    public async Task<List<BatteryPack>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appContext.BatteryPacks.Where(b => b.UserId == userId).ToListAsync(cancellationToken);
    }
}
