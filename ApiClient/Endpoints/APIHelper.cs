using ApiClient.DataTransferObjects;
using Microsoft.Extensions.Configuration;
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

    public APIHelper(IConfiguration config, ILoggedInUserModel loggedInUser, IHttpClientFactory apiClient)
    {
        _config = config;
        _loggedInUser = loggedInUser;
        _apiClient = apiClient;
        ApiClient = apiClient.CreateClient("apiClient");
    }

    public HttpClient ApiClient { get; private set; }

    public async Task<AuthenticatedUser> Authenticate(string username, string password)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

        var tokenPath = _config["tokenEndpoint"];
        var client = _apiClient.CreateClient("apiClient");

        using HttpResponseMessage response = await client.PostAsync(tokenPath, data);
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
        ApiClient.DefaultRequestHeaders.Clear();
    }

    public async Task GetLoggedInUserInfo(string token)
    {
        ApiClient.DefaultRequestHeaders.Clear();
        ApiClient.DefaultRequestHeaders.Accept.Clear();
        ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        using HttpResponseMessage response = await ApiClient.GetAsync("/api/User");
        if (response.IsSuccessStatusCode == false)
        {
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
