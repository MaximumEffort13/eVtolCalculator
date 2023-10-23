using ApiClient.DataTransferObjects.IdentityObjects;
using FluentResults;
using Polly.Retry;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiClient.Abstractions;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using ApiClient.Models;

namespace ApiClient.Endpoints;

public class UserEndpoints : IUserEndpoints
{
    private readonly IApiHelper _apiHelper;
    private readonly ILogger<UserEndpoints> _logger;

    public UserEndpoints(IApiHelper apiHelper, ILogger<UserEndpoints> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger;
    }

    public async Task<Result> RegisterUser(CreateUserModel user)
    {
        var data = new
        {
            user.FirstName,
            user.LastName,
            user.EmailAddress,
            user.PhoneNumber,
            user.Password,
            user.StreetName,
            user.Suburb,
            user.City,
            user.Province,
            user.Country,
            user.PostalCode
        };

        var cancellationToken = new CancellationToken();

        AsyncRetryPolicy policy = Policy.Handle<Exception>().RetryAsync(3);

        PolicyResult<HttpResponseMessage> result = await policy.ExecuteAndCaptureAsync(() => _apiHelper.Client.PostAsJsonAsync($"/api/user/register", user, cancellationToken));

        if (result.Result is null || result.Result.IsSuccessStatusCode == false)
        {
            return Result.Fail(result.FinalException.ToString());
        }

        if (result.Result.Content is null)
        {
            return Result.Fail("User registration succeeded but no data returned from the server.");
        }

        return Result.Ok();
    }

}
