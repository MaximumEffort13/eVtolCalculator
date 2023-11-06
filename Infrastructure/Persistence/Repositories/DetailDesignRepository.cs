using Domain.Entities.DetailedDesign;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;
using Domain.Abstractions;

namespace Infrastructure.Persistence.Repositories;

public sealed class ElectricVtolRepository : IElectricVtolRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public ElectricVtolRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(ElectricVtolDesign electricVtolDesign)
    {
        _appDbContext.ElectricVtolDesigns.Add(electricVtolDesign);
    }

    public async Task<ElectricVtolDesign> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.ElectricVtolDesigns.SingleAsync(eVtol => eVtol.Id == id && eVtol.UserId == userId, cancellationToken);
    }

    public async Task<List<ElectricVtolDesign>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.ElectricVtolDesigns.Where(eVtol => eVtol.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<ElectricVtolDesign> GetByNameAsync(string name, Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.ElectricVtolDesigns.SingleAsync(eVtol => eVtol.Name == name && eVtol.UserId == userId, cancellationToken);
    }
}
