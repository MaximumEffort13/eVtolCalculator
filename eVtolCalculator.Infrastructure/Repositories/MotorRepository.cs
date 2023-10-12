using Domain.Entities.DetailedDesign;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Repositories;

public sealed class MotorRepository : IMotorRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public MotorRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(Motor motor)
    {
        _appDbContext.Motors.Add(motor);
    }

    public async Task<Motor> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.SingleAsync(cell => cell.Id == id, cancellationToken);
    }

    public async Task<List<Motor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.ToListAsync(cancellationToken);
    }
}
