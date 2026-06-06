using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.IdentityObjects;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace eVtolCalculatorUi.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IApiHelper _apiHelper;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly string authTokenStorageKey;
    private readonly string authRefreshTokenStorageKey;

    public AuthenticationService(IApiHelper apiHelper,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorage,
                                 IConfiguration config)
    {
        _apiHelper = apiHelper;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
        _config = config;
        authTokenStorageKey = _config["authTokenStorageKey"]!;
        authRefreshTokenStorageKey = _config["authRefreshTokenStorageKey"]!;
    }

    public async Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication)
    {
        var authResult = await _apiHelper.Authenticate(userForAuthentication);

        if (authResult.IsSuccess == false)
        {
            return null;
        }

        await _localStorage.SetItemAsync(authTokenStorageKey, authResult.Value.AccessToken);
        await _localStorage.SetItemAsync(authRefreshTokenStorageKey, authResult.Value.RefreshToken);

        var authenticationSuccessful = await ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Value.AccessToken);

        return authenticationSuccessful ? authResult.Value : null;
    }

    public async Task<AuthenticatedUserModel?> RefreshAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(authTokenStorageKey);
        var refreshToken = await _localStorage.GetItemAsync<string>(authRefreshTokenStorageKey);

        var refreshResult = await _apiHelper.RefreshAuthentication(token, refreshToken);

        if (refreshResult is null || refreshResult.IsFailed)
        {
            await Logout();
            await _localStorage.RemoveItemAsync(authTokenStorageKey);
            await _localStorage.RemoveItemAsync(authRefreshTokenStorageKey);

            return new AuthenticatedUserModel();
        }

        await _localStorage.SetItemAsync(authTokenStorageKey, refreshResult.Value.AccessToken);
        await _localStorage.SetItemAsync(authRefreshTokenStorageKey, refreshResult.Value.RefreshToken);

        var authenticationSuccessful = await ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(refreshResult.Value.AccessToken);

        return authenticationSuccessful ? refreshResult.Value : null;
    }

    public async Task Logout()
    {
        await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}
