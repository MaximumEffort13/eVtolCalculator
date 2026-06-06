using Domain.Primitives;

namespace Domain.Entities.AuthenticationModels;

public sealed class PersonEntity : Entity
{
    public PersonEntity(Guid id, Guid userId, string firstName, string lastName, string phoneNumber) : base(id)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; private set; }
    public Guid UserId { get; private set; }
}
