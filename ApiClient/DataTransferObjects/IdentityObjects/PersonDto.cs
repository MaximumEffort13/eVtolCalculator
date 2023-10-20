namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed class PersonDto
{
    public Guid Id { get; set; }
    public string FirsName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<AddressDto> Addresses { get; set; }
}
