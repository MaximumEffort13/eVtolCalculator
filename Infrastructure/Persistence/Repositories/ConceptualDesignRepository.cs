using Domain.Entities.ConceptDesign;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using Infrastructure.Persistence.DataAccess;

namespace Infrastructure.Persistence.Repositories;

public sealed class ConceptualDesignRepository : IConceptualDesignRepository
{
    private readonly ApplicationDbContext _appContext;

    public ConceptualDesignRepository(ApplicationDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<ConceptualVtolDesign> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appContext.ConceptualDesign.SingleAsync(a => a.Id == id, cancellationToken = default);
    }

    public async Task<List<ConceptualVtolDesign>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appContext.ConceptualDesign.ToListAsync(cancellationToken);
    }

    public void Insert(ConceptualVtolDesign parameters)
    {
        _appContext.ConceptualDesign.Add(parameters);
    }
}
