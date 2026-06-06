using Domain.Primitives;

namespace Domain.Entities.AuthenticationModels;

public sealed class AddressEntity : Entity
{
    public AddressEntity(Guid id, Guid personId, string streetName, string suburb, string city, string province, string postalCode) : base(id)
    {
        PersonId = personId;
        StreetName = streetName;
        Suburb = suburb;
        City = city;
        Province = province;
        PostalCode = postalCode;
    }

    public Guid PersonId { get; private set; }
    public string StreetName { get; private set; }
    public string Suburb { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string PostalCode { get; private set;}
}
