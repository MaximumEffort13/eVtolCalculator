using ApiClient.DataTransferObjects;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace eVtolCalculatorUi.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly string authTokenStorageKey;
    private readonly string authTokenRefreshStorageKey;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IHttpClientFactory clientFactory,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorage,
                                 IConfiguration config,
                                 ILogger<AuthenticationService> logger)
    {
        _clientFactory = clientFactory;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
        _config = config;
        authTokenStorageKey = _config["authTokenStorageKey"];
        authTokenRefreshStorageKey = _config["authTokenRefreshStorageKey"];
        _logger = logger;
    }

    public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
    {
        var client = _clientFactory.CreateClient("apiClient");

        var authResult = await client.PostAsJsonAsync("/account/login", userForAuthentication);

        if (authResult.IsSuccessStatusCode == false)
        {
            _logger.LogWarning($"{authResult.RequestMessage.RequestUri} not found.");
            return null;
        }

        var authContent = await authResult.Content.ReadFromJsonAsync<AuthenticatedUserModel>();
        
        if(authContent is null)
        {
            _logger.LogWarning($"{authResult.RequestMessage.RequestUri} response incorrect.");
            return null; ;
        }

        await _localStorage.SetItemAsync(authTokenStorageKey, authContent.AccessToken);
        await _localStorage.SetItemAsync(authTokenRefreshStorageKey, authContent.RefreshToken);

        await ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication( authContent.AccessToken);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authContent.AccessToken);

        return authContent;
    }

    public async Task Logout()
    {
        await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}
