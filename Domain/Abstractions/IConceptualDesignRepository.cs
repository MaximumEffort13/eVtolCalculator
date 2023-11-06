using Domain.Entities.ConceptDesign;

namespace Domain.Abstractions;

public interface IConceptualDesignRepository
{
    Task<List<ConceptualVtolDesign>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<ConceptualVtolDesign> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
    void Insert(ConceptualVtolDesign parameters);
}