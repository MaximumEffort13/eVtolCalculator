using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class BatteryModuleRepository : IBatteryModuleRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public BatteryModuleRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void CreateBatteryModule(BatteryModule module)
    {
        _appDbContext.BatteryModules.Add(module);
    }

    public async Task<BatteryModule> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.BatteryModules.SingleAsync(mod => mod.Id == id, cancellationToken);
    }

    public async Task<List<BatteryModule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.BatteryModules.ToListAsync(cancellationToken);
    }
}
