using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.IdentityObjects;
using ApiClient.Endpoints;
using FluentResults;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using Polly;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ApiClient;

public sealed class ApiHelper : IApiHelper
{
    private readonly ILogger<ApiHelper> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggedInUserModel _loggedInUser;
    private readonly IPersonEndpoints _personEndpoints;

    public ApiHelper(ILogger<ApiHelper> logger, IHttpClientFactory httpClientFactory, ILoggedInUserModel loggedInUser)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _loggedInUser = loggedInUser;
        Client = InitializeClient();
    }

    public HttpClient Client { get; private set; }

    private HttpClient InitializeClient()
    {
        var client = _httpClientFactory.CreateClient("apiClient");

        if (string.IsNullOrEmpty(_loggedInUser.Token) == false)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loggedInUser.Token);
        }

        return client;
    }

    public void UnothorizeClient()
    {
        Client.DefaultRequestHeaders.Clear();
    }


    public async Task<Result<AuthenticatedUserModel>> Authenticate(AuthenticationUserModel loginUser)
    {
        var cancellationToken = new CancellationToken();

        AsyncRetryPolicy policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attemtps => TimeSpan.FromMilliseconds(50 * attemtps));

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => Client.PostAsJsonAsync($"/account/login", loginUser, cancellationToken));

        if (result.Result.IsSuccessStatusCode == false)
        {
            _logger.LogWarning("Login failed.");
            return Result.Fail<AuthenticatedUserModel>(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            _logger.LogWarning("Could not parse response from server.");
            return Result.Fail("Login succeeded but no data returned from the server.");
        }

        var response = await result.Result.Content.ReadFromJsonAsync<AuthenticatedUserModel>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<AuthenticatedUserModel>("Invalid Data Returned From Server");
        }

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);

        return Result.Ok(response);
    }

    public async Task<Result<AuthenticatedUserModel>> RefreshLogin(string refreshToken)
    {
        var cancellationToken = new CancellationToken();

        RefreshLoginCommand refreshCommand = new (refreshToken);

        AsyncRetryPolicy policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attemtps => TimeSpan.FromMilliseconds(50 * attemtps));

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => Client.PostAsJsonAsync($"/account/refresh", refreshCommand, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail<AuthenticatedUserModel>(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            return Result.Fail("Refresh succeeded but no data returned from the server.");
        }

        var response = await result.Result.Content.ReadFromJsonAsync<AuthenticatedUserModel>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<AuthenticatedUserModel>("Invalid or no data returned from server.");
        }

        _loggedInUser.Token = response.AccessToken;
        _loggedInUser.RefreshToken = response.RefreshToken;

        return Result.Ok(response);
    }


    public async Task<Result> GetLoggedInUserInfo(string token)
    {
        var cancellationToken = new CancellationToken();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AsyncRetryPolicy policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attemtps => TimeSpan.FromMilliseconds(50 * attemtps));

        PolicyResult<PersonDto?> result = await policy.ExecuteAndCaptureAsync(() => Client.GetFromJsonAsync<PersonDto>($"/api/Person", cancellationToken));


        if (result is null)
        {
            return Result.Fail("Could not contact server");
        }

        if (result.FaultType is not null)
        {
            return Result.Fail($"Exception occurred during the request to the server.\n{result.FinalException.Message}");
        }

        var response = result.Result;

        _loggedInUser.FirstName = response.FirsName;
        _loggedInUser.LastName = response.LastName;
        _loggedInUser.EmailAddress = response.Email;
        _loggedInUser.Id = response.Id;

        return Result.Ok();
    }

    public void Logout()
    {
        UnothorizeClient();

        _loggedInUser.Token = string.Empty;
        _loggedInUser.EmailAddress = string.Empty;
        _loggedInUser.RefreshToken = string.Empty;
        _loggedInUser.StreetName = string.Empty;
        _loggedInUser.Province = string.Empty;
        _loggedInUser.City = string.Empty;
        _loggedInUser.PostalCode = string.Empty;
        _loggedInUser.FirstName = string.Empty;
        _loggedInUser.LastName = string.Empty;
    }
}
