using Domain.Entities.DetailedDesign.Battery;

namespace Domain.Abstractions;

public interface IBatteryPackRepository
{
    void CreateBatteryPack(BatteryPack battery);
    Task<List<BatteryPack>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<BatteryPack> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    Task<BatteryPack> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken);
}