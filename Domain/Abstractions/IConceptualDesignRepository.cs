using Domain.Entities.ConceptDesign;

namespace Domain.Abstractions;

public interface IConceptualDesignRepository
{
    Task<ConceptualVtolDesign> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Insert(ConceptualVtolDesign parameters);
}