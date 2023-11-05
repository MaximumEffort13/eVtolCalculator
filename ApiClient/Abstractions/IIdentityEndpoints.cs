using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;

namespace ApiClient.Abstractions;

public interface IIdentityEndpoints
{
    Task<Result> ConfirmAccountEmail(GetAccountConfirmEmailQuery confirmationEmail);
    Task<Result<ManageAccountInfoResponse>> GetManageAccount();
    Task<Result<ManageAccountInfoResponse>> PostManageAccount(AccountManageInfoCommand account);
}