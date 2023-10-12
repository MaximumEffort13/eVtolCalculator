using Infrastructure.DataAccess;
using Domain.Abstractions;

namespace Infrastructure.Repositories
{
    public sealed class DetailDesignRepository
    {
        private readonly ApplicationDbContext _appContext;

        public DetailDesignRepository(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        //public async Task<ElectricVtolDesign> GetById(Guid id)
        //{
        //    _appContext.
        //}
    }
}
