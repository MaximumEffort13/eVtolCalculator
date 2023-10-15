using Domain.Entities.DetailedDesign;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Repositories;

public sealed class InvererRepository : IInvererRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public InvererRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(Inverter inverter)
    {
        _appDbContext.Inverters.Add(inverter);
    }

    public async Task<Inverter> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<List<Inverter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.ToListAsync(cancellationToken);
    }

    public async Task<Inverter> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Name == name, cancellationToken);
    }
}
