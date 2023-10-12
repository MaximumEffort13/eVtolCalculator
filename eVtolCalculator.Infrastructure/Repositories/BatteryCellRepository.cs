using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class BatteryCellRepository : IBatteryCellRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public BatteryCellRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void CreateBatteryCell(Cell cell)
    {
        _appDbContext.Cells.Add(cell);
    }

    public async Task<Cell> GetCellByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Cells.SingleAsync(cell => cell.Id == id, cancellationToken);
    }

    public async Task<List<Cell>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Cells.ToListAsync(cancellationToken);
    }
}
