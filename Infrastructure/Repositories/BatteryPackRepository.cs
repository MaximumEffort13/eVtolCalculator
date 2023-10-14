using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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

    public async Task<BatteryPack> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appContext.BatteryPacks.SingleAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<List<BatteryPack>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appContext.BatteryPacks.ToListAsync(cancellationToken);
    }
}
