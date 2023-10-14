using Domain.Entities.DetailedDesign.Battery;

namespace Domain.Abstractions;

public interface IBatteryPackRepository
{
    void CreateBatteryPack(BatteryPack battery);
    Task<List<BatteryPack>> GetAllAsync(CancellationToken cancellationToken);
    Task<BatteryPack> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}