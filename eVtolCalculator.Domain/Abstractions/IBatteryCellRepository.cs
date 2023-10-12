using Domain.Entities.DetailedDesign.Battery;

namespace Domain.Abstractions;

public interface IBatteryCellRepository
{
    void CreateBatteryCell(Cell cell);
    Task<List<Cell>> GetAllAsync(CancellationToken cancellationToken);
    Task<Cell> GetCellByIdAsync(Guid id, CancellationToken cancellationToken);
}