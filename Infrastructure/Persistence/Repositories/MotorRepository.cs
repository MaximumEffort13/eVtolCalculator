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

    public async Task<Motor> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.SingleAsync(m => m.Id == id && m.UserId == userId, cancellationToken);
    }

    public async Task<List<Motor>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.Where(m => m.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Motor> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Motors.SingleAsync(m => m.Name == name && m.UserId == userId, cancellationToken);
    }
}
