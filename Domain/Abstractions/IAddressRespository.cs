using Domain.Entities.AuthenticationModels;

namespace Domain.Abstractions;

public interface IAddressRespository
{
    void Create(AddressEntity address);
    Task<List<AddressEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<AddressEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AddressEntity> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken);
}