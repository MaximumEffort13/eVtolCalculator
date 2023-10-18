using Domain.Entities.ConceptDesign;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;

namespace Infrastructure.Persistence.Repositories;

public sealed class MissionParameterRepository : IMissionParameterRepository
{
    private readonly ApplicationDbContext _appDbContext;

    public MissionParameterRepository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(MissionParameterEstimates mission)
    {
        _appDbContext.MissionParameters.Add(mission);
    }

    public async Task<MissionParameterEstimates> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.MissionParameters.SingleAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<List<MissionParameterEstimates>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.MissionParameters.ToListAsync(cancellationToken);
    }
}