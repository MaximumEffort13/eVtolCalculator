namespace ApiClient.Abstractions;

public interface ILoggedInUserModel
{
    Guid Id { get; set; }
    string AccessToken { get; set; }
    string RefreshToken { get; set; }
    string EmailAddress { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string StreetName { get; set; }
    string Province { get; set; }
    string City { get; set; }
    string PostalCode { get; set; }
}