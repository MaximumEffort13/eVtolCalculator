using Application.DTO;
using Domain.Entities.AuthenticationModels;

namespace Application.Mappers;
public static class PersonDtoMapper
{
    public static PersonDto Map(PersonEntity person, AddressEntity addresses)
    {
        return new PersonDto
        {
            Id = person.Id,
            FirsName = person.FirstName,
            LastName = person.LastName,
            PhoneNumber = person.PhoneNumber,
            Addresses = new List<AddressEntity> { addresses }
        };
    }
}
