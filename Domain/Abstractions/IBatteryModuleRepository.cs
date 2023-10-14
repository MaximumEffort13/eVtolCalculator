using Domain.Entities.DetailedDesign.Battery;

namespace Domain.Abstractions;

public interface IBatteryModuleRepository
{
    void CreateBatteryModule(BatteryModule module);
    Task<List<BatteryModule>> GetAllAsync(CancellationToken cancellationToken);
    Task<BatteryModule> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}