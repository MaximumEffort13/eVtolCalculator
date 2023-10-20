namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed record RegisterUserCommand(string Email, string Password);