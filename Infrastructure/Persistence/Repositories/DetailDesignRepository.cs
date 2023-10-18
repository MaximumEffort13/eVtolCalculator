using Domain.Entities.DetailedDesign;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.DataAccess;
using Infrastructure.Repositories;

namespace Infrastructure.Persistence.Repositories
{
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

        public async Task<ElectricVtolDesign> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _appDbContext.ElectricVtolDesigns.SingleAsync(eVtol => eVtol.Id == id, cancellationToken);
        }

        public async Task<List<ElectricVtolDesign>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.ElectricVtolDesigns.ToListAsync(cancellationToken);
        }

        public async Task<ElectricVtolDesign> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _appDbContext.ElectricVtolDesigns.SingleAsync(eVtol => eVtol.Name == name, cancellationToken);
        }
    }
}
