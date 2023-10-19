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

    public async Task<InverterEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<List<InverterEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.ToListAsync(cancellationToken);
    }

    public async Task<InverterEntity> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _appDbContext.Inverters.SingleAsync(i => i.Name == name, cancellationToken);
    }
}
