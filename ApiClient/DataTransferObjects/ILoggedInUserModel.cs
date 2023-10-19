
namespace ApiClient.DataTransferObjects;

public interface ILoggedInUserModel
{
    Guid Id { get; set; }
    string EmailAddress { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    DateTime SubscribedDate { get; set; }
    string Token { get; set; }
}