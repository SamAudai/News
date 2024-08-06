using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace News.Client.Authentication
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _annonymous;

        public AppAuthenticationStateProvider(HttpClient httpClient, 
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _annonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if(await _localStorage.GetItemAsync<string>("remToken") != "isPersistent")
            {
                await _localStorage.RemoveItemAsync("appToken");
            }
            var token = await _localStorage.GetItemAsync<string>("appToken");
            if(string.IsNullOrWhiteSpace(token))
            {
                return _annonymous;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),"jwtAuthType")));
        }

        public void NotifyUserAuthentication(string name)
        {
            var authUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(name), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_annonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
