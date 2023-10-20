namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed record AccountManageInfoCommand(string NewEmail, string NewPassword, string OldPassword);
