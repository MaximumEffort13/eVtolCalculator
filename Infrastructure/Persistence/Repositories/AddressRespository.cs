using Domain.Abstractions;
using Domain.Entities.AuthenticationModels;
using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class AddressRespository : IAddressRespository
{
    private readonly ApplicationDbContext _appDbContext;

    public AddressRespository(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Create(AddressEntity address)
    {
        _appDbContext.Addresses.Add(address);
    }

    public async Task<AddressEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Addresses.SingleAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<AddressEntity> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Addresses.SingleAsync(a => a.PersonId == personId, cancellationToken);
    }

    public async Task<List<AddressEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _appDbContext.Addresses.ToListAsync(cancellationToken);
    }

}