using ApiClient.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Endpoints;

public class APIHelper : IAPIHelper
{
    private readonly IConfiguration _config;
    private readonly ILoggedInUserModel _loggedInUser;
    private readonly IHttpClientFactory _apiClient;
    private readonly ILogger<APIHelper> _logger;

    public APIHelper(
        IConfiguration config,
        ILoggedInUserModel loggedInUser,
        IHttpClientFactory apiClient,
        ILogger<APIHelper> logger)
    {
        _config = config;
        _loggedInUser = loggedInUser;
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<AuthenticatedUser> Authenticate(string username, string password)
    {
        var client = _apiClient.CreateClient("apiClient");

        AuthenticationUserModel userModel = new()
        {
            Email = username,
            Password = password
        };

        using HttpResponseMessage response = await client.PostAsJsonAsync("/account/login", userModel);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthenticatedUser>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public void LogOffUser()
    {
        var client = _apiClient.CreateClient("apiClient");
        client.DefaultRequestHeaders.Clear();
    }

    public async Task GetLoggedInUserInfo(string token)
    {
        var client = _apiClient.CreateClient("apiClient");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var cancellationToken = new CancellationToken();
        
        using HttpResponseMessage response = await client.GetAsync($"/account", cancellationToken);
        if (response.IsSuccessStatusCode == false)
        {
            _logger.LogWarning("User not authenticated. Please login");
            throw new Exception(response.ReasonPhrase);
        }

        var result = await response.Content.ReadFromJsonAsync<LoggedInUserModel>() ?? throw new Exception("Could not decode received data from server.");

        _loggedInUser.FirstName = result.FirstName;
        _loggedInUser.LastName = result.LastName;
        _loggedInUser.Id = result.Id;
        _loggedInUser.SubscribedDate = result.SubscribedDate;
        _loggedInUser.EmailAddress = result.EmailAddress;
        _loggedInUser.Token = token;
    }
}
