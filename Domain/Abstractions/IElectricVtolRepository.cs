using Domain.Entities.DetailedDesign;

namespace Infrastructure.Repositories
{
    public interface IElectricVtolRepository
    {
        void Create(ElectricVtolDesign electricVtolDesign);
        Task<List<ElectricVtolDesign>> GetAllAsync(CancellationToken cancellationToken);
        Task<ElectricVtolDesign> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ElectricVtolDesign> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}