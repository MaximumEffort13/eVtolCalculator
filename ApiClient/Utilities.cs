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

    public async Task<Result<T?>> GetRequestAsync<T>(string apiEndpoint) 
        where T: class 
    {
        CancellationToken cancellationToken = new ();

        _logger.LogInformation(_apiHelper.Client.BaseAddress!.ToString());

        T? response = await _apiHelper.Client.GetFromJsonAsync<T?>(apiEndpoint, cancellationToken);

        if (response is null)
        {
            return Result.Fail("Could not contact server");
        }

        return response is not null ? response: Result.Fail("Invalid data returned from server.");
    }

    public async Task<Result> GetRequestAsync(string apiEndpoint)
    {
        CancellationToken cancellationToken = new();

        HttpResponseMessage response =  await _apiHelper.Client.GetAsync(apiEndpoint, cancellationToken);

        if (response is null)
        {
            return Result.Fail("Could not contact server");
        }

        if (response.IsSuccessStatusCode is false)
        {
            return Result.Fail($"Exception occurred during the request to the server.");
        }

        return response is not null ? Result.Ok() : Result.Fail("Invalid data returned from server.");
    }

    public async Task<Result> PostCommandAsync<TInput>(TInput commandBodyObject, string apiEndpoint)
    where TInput : class
    {
        CancellationToken cancellationToken = new();

        HttpResponseMessage result = await _apiHelper.Client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken);

        if (result is null || result.IsSuccessStatusCode == false)
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

        HttpResponseMessage result = await _apiHelper.Client.PostAsJsonAsync(apiEndpoint, commandBodyObject, cancellationToken);

        if (result is null || result.IsSuccessStatusCode == false)
        {
            return Result.Fail<TOutput>(result.ReasonPhrase);
        }

        if (result.Content is null)
        {
            return Result.Fail("The expected response was not received.");
        }

        var response = await result.Content.ReadFromJsonAsync<TOutput>(cancellationToken);

        if (response is null)
        {
            return Result.Fail<TOutput>("Invalid or no data returned from server.");
        }

        return response;
    }
}
