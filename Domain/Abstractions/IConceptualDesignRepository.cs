using Domain.Entities.ConceptDesign;

namespace Domain.Abstractions;

public interface IConceptualDesignRepository
{
    Task<ConceptualVtolDesign> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<ConceptualVtolDesign>> GetAllAsync(CancellationToken cancellationToken);
    void Insert(ConceptualVtolDesign parameters);
}