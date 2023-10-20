using ApiClient.Abstractions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace eVtolCalculatorUi.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly IApiHelper _apiHelper;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(ILocalStorageService localStorage,
                             IConfiguration config,
                             IApiHelper apiHelper)
    {
        _localStorage = localStorage;
        _config = config;
        _apiHelper = apiHelper;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string authTokenStorageKey = _config["authTokenStorageKey"]!;
        string authRefreshTokenStorageKey = _config["authRefreshTokenStorageKey"]!;

        var token = await _localStorage.GetItemAsync<string>(authTokenStorageKey);
        var refreshToken = await _localStorage.GetItemAsync<string>(authRefreshTokenStorageKey);

        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymous;
        }

        bool isAuthenticated =  await NotifyUserAuthentication(token);

        if (isAuthenticated is false)
        {
            return _anonymous;
        }

        _apiHelper.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(
            new ClaimsPrincipal(
            new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }

    public async Task<bool> NotifyUserAuthentication(string token)
    {
        bool isAuthenticatedoutput;
        Task<AuthenticationState> authState;
        try
        {
            await _apiHelper.GetLoggedInUserInfo(token);
            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));

            authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            isAuthenticatedoutput = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await NotifyUserLogout();
            isAuthenticatedoutput = false;
        }
        return isAuthenticatedoutput;
    }

    public async Task NotifyUserLogout()
    {
        string authTokenStorageKey = _config["authTokenStorageKey"];
        string authTokenRefreshStorageKey = _config["authRefreshTokenStorageKey"];
        await _localStorage.RemoveItemAsync(authTokenStorageKey);
        await _localStorage.RemoveItemAsync(authTokenRefreshStorageKey);

        var authState = Task.FromResult(_anonymous);
        _apiHelper.Logout();
        _apiHelper.Client.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(authState);
    }
}
