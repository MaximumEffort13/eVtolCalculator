using Domain.Entities.DetailedDesign;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using Infrastructure.Persistence.DataAccess;

namespace Infrastructure.Persistence.Repositories;

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
        return await _appDbContext.Motors.SingleAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<List<Motor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.ToListAsync(cancellationToken);
    }

    public async Task<Motor> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.SingleAsync(m => m.Name == name, cancellationToken);
    }
}
