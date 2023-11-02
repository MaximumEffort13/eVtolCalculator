using FluentResults;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using Polly;
using System.Net.Http.Json;

namespace ApiClient;

/// <summary>
/// A shared type that will be used by all endpoints in client.
/// </summary>
public class Utilities
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<Utilities> _logger;

    public Utilities(IHttpClientFactory httpClientFactory, ILogger<Utilities> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<Result<T>> GetRequestAsync<T>(string apiEndpoint) 
        where T: class 
    {
        CancellationToken cancellationToken = new ();
        var client = _httpClientFactory.CreateClient("userClient");

        _logger.LogInformation(client.BaseAddress!.ToString());

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<T?> response = await policy.ExecuteAndCaptureAsync(() => client.GetFromJsonAsync<T>(apiEndpoint, cancellationToken));

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
        var client = _httpClientFactory.CreateClient("userClient");

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> response = await policy.ExecuteAndCaptureAsync(async () => await client.GetAsync(apiEndpoint, cancellationToken));

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
        var client = _httpClientFactory.CreateClient("userClient");

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail(result.FinalException.ToString());
        }

        return Result.Ok();
    }


    public async Task<Result<TOutput>> PostCommandAsync<TInput, TOutput>(TInput commandBodyObject, string apiEndpoint)
        where TInput : class
        where TOutput : class
    {
        CancellationToken cancellationToken = new ();
        var client = _httpClientFactory.CreateClient("userClient");

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken));

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
