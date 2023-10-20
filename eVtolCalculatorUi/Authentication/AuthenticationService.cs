using ApiClient.Abstractions;
using ApiClient.DataTransferObjects.IdentityObjects;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eVtolCalculatorUi.Authentication
{
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

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {
            var authResult = await _apiHelper.Authenticate(userForAuthentication);

            if (authResult.IsSuccess == false)
            {
                return null;
            }

            await _localStorage.SetItemAsync(authTokenStorageKey, authResult.Value.AccessToken);

            await ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResult.Value.AccessToken);

            return authResult.Value;
        }

        public async Task Logout()
        {
            await ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }
}
