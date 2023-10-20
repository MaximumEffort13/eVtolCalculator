namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed record GetAccountConfirmEmailQuery(string UserId, string Code, string ChangedEmail = "");