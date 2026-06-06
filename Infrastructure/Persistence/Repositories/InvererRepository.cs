using Domain.Entities.DetailedDesign;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using Infrastructure.Persistence.DataAccess;

namespace Infrastructure.Persistence.Repositories;

public sealed class InvererRepository : IInvererRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public InvererRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(InverterEntity inverter)
    {
        _appDbContext.Inverters.Add(inverter);
    }

    public async Task<InverterEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Id == id && i.UserId == userId, cancellationToken);
    }

    public async Task<List<InverterEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.Where(i => i.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<InverterEntity> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Name == name && i.UserId == userId, cancellationToken);
    }
}
