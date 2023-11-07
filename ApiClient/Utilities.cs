using FluentResults;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using Polly;
using System.Net.Http.Json;
using ApiClient.Abstractions;

namespace ApiClient;

/// <summary>
/// A shared type that will be used by all endpoints in client.
/// </summary>
public class Utilities
{
    private readonly IApiHelper _apiHelper;
    private readonly ILogger<Utilities> _logger;

    public Utilities(IApiHelper apiHelper, ILogger<Utilities> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<Result<T>> GetRequestAsync<T>(string apiEndpoint) 
        where T: class 
    {
        CancellationToken cancellationToken = new ();

        _logger.LogInformation(_apiHelper.Client.BaseAddress!.ToString());

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<T?> response = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.GetFromJsonAsync<T>(apiEndpoint, cancellationToken));

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

    public async Task<Result> GetRequestAsync(string apiEndpoint)
    {
        CancellationToken cancellationToken = new();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> response = await policy.ExecuteAndCaptureAsync(async () => await _apiHelper.Client.GetAsync(apiEndpoint, cancellationToken));

        if (response is null)
        {
            return Result.Fail("Could not contact server");
        }

        if (response.FaultType is not null)
        {
            return Result.Fail($"Exception occurred during the request to the server.\n{response.FinalException.Message}");
        }

        return response.Result is not null ? Result.Ok() : Result.Fail("Invalid data returned from server.");
    }

    public async Task<Result> PostCommandAsync<TInput>(TInput commandBodyObject, string apiEndpoint)
    where TInput : class
    {
        CancellationToken cancellationToken = new();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail("Error during the request to the server.");
        }

        return Result.Ok();
    }


    public async Task<Result<TOutput>> PostCommandAsync<TInput, TOutput>(TInput commandBodyObject, string apiEndpoint)
        where TInput : class
        where TOutput : class
    {
        CancellationToken cancellationToken = new ();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail<TOutput>(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            return Result.Fail("The expected response was not received.");
        }

        var response = await result.Result.Content.ReadFromJsonAsync<TOutput>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<TOutput>("Invalid or no data returned from server.");
        }

        return response;
    }
}
