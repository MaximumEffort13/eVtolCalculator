namespace ApiClient.DataTransferObjects.IdentityObjects;
public sealed record LoginUserCommand(string Email,
                                    string Password,
                                    string TwoFactorCode,
                                    string TwoFactorRecoveryCode);
