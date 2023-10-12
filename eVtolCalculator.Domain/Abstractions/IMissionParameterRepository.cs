using Domain.Entities.ConceptDesign;

namespace Domain.Abstractions;

public interface IMissionParameterRepository
{
    void Create(MissionParameterEstimates mission);
    Task<List<MissionParameterEstimates>> GetAllAsync(CancellationToken cancellationToken);
    Task<MissionParameterEstimates> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}