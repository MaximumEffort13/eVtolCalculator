using Domain.Entities.ConceptDesign;

namespace Domain.Abstractions;

public interface IMissionParameterRepository
{
    void Create(MissionParameterEstimates mission);
    Task<List<MissionParameterEstimates>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task<MissionParameterEstimates> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}