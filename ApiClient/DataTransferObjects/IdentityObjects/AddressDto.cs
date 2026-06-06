namespace ApiClient.DataTransferObjects.IdentityObjects;

public class AddressDto
{
    public Guid PersonId { get; private set; }
    public string StreetName { get; private set; }
    public string Suburb { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string PostalCode { get; private set; }
}
