using Domain.Entities.AuthenticationModels;

namespace Application.DTO;
public sealed class PersonDto
{
    public string Id { get; set; }
    public string FirsName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<AddressEntity> Addresses { get; set; }
}
