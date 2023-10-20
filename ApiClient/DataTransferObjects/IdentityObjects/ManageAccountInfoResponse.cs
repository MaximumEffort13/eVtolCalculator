namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed record ManageAccountInfoResponse(string Email, bool IsEmailConfirmed, Claims Claims);
