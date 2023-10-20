using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ApiClient.Endpoints;

public class IdentityEndpoints : IIdentityEndpoints
{
    private readonly ILogger<IdentityEndpoints> _logger;
    private readonly ILoggedInUserModel _currentUser;
    private readonly IApiHelper _apiHelper;

    public IdentityEndpoints(ILogger<IdentityEndpoints> logger,
                             ILoggedInUserModel currentUser,
                             IApiHelper apiHelper)
    {
        _logger = logger;
        _currentUser = currentUser;
        _apiHelper = apiHelper;
    }


    public async Task<Result<RegisterUserResponse>> RegisterUser(RegisterUserCommand user)
    {
        var cancellationToken = new CancellationToken();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.PostAsJsonAsync($"/account/register", user, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail<RegisterUserResponse>(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            return Result.Fail("Refresh succeeded but no data returned from the server.");
        }

        var response = await result.Result.Content.ReadFromJsonAsync<RegisterUserResponse>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<RegisterUserResponse>("Invalid or no data returned from server.");
        }

        return response;
    }

    public async Task<Result> ResendEmailConfirmation(ResendConfirmationEmailCommand command)
    {
        var cancellationToken = new CancellationToken();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() =>
            _apiHelper.Client.PostAsJsonAsync($"resendConfirmationEmail", command, cancellationToken)
        );

        if (result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail("Email could not be sent.");
        }

        return Result.Ok();
    }


    public async Task<Result<ManageAccountInfoResponse>> PostManageAccount(AccountManageInfoCommand account)
    {
        var cancellationToken = new CancellationToken();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.PostAsJsonAsync($"/account/manage/info", account, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail<ManageAccountInfoResponse>(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            return Result.Fail("Refresh succeeded but no data returned from the server.");
        }

        var response = await result.Result.Content.ReadFromJsonAsync<ManageAccountInfoResponse>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<ManageAccountInfoResponse>("Invalid or no data returned from server.");
        }

        return response;
    }

    public async Task<Result<ManageAccountInfoResponse>> GetManageAccount()
    {
        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<ManageAccountInfoResponse?> response = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.GetFromJsonAsync<ManageAccountInfoResponse>("/account/manage/info"));

        if (response is null)
        {
            return Result.Fail("Could not contact server");
        }

        if (response.FaultType is not null)
        {
            return Result.Fail($"Exception occurred during the request to the server.\n{response.FinalException.Message}");
        }

        return response.Result is not null ? response.Result : Result.Fail("Invalid data returned from server.");
    }


    public async Task<Result> ConfirmAccountEmail(GetAccountConfirmEmailQuery confirmationEmail)
    {
        var cancellationToken = new CancellationToken();
        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() =>
        _apiHelper.Client.GetAsync($"/account/confirmEmail?userId={confirmationEmail.UserId}&code={confirmationEmail.Code}&emailChanged:{confirmationEmail.ChangedEmail}"));

        if (result.Result is null || result.FaultType is not null)
        {
            return Result.Fail($"Error encountered during request to server. \n{result.FinalException}");
        }

        if (result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail($"The request to the server failed. \n{result.Result.StatusCode}");
        }

        return Result.Ok();
    }
}
