using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using News.Shared.DTOs.Account;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using News.Shared.Models;
using News.Shared.DTOs.Adminstration;
using System.Net;

namespace News.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly AuthenticationStateProvider _stateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public AuthenticationService(HttpClient httpClient,
            AuthenticationStateProvider stateProvider,
            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _stateProvider = stateProvider;
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userRegistration)
        {
            var result = await _httpClient.PostAsJsonAsync<UserRegistrationDto>("api/Account/RegisterUser", userRegistration);
            if (!result.IsSuccessStatusCode)
            {
                var resMsg = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<RegistrationResponseDto>(resMsg, _jsonSerializerOptions);
                //throw new Exception(resMsg);
                return msg;
            }
            return new RegistrationResponseDto { isSuccess = true };
        }

        public async Task<LoginResponsDto> LoginUser(LoginDto login)
        {
            var result = await _httpClient.PostAsJsonAsync<LoginDto>("api/Account/Login", login);
            var resMsg = await result.Content.ReadAsStringAsync();
            var msg = JsonSerializer.Deserialize<LoginResponsDto>(resMsg, _jsonSerializerOptions);
            if (!result.IsSuccessStatusCode)
            {
                //throw new Exception(resMsg);
                return msg;
            }
            await _localStorage.SetItemAsync("appToken", msg.Token);
            ((AppAuthenticationStateProvider)_stateProvider).NotifyUserAuthentication(msg.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", msg.Token);
            if(login.RemmemberMe)
            {
                await _localStorage.SetItemAsync("remToken", "isPersistent");
            }
            else
            {
                await _localStorage.RemoveItemAsync("remToken");
            }
            return new LoginResponsDto { isLogin = true };
        }

        public async Task LogoutUser()
        {
            await _localStorage.RemoveItemAsync("appToken");
            await _localStorage.RemoveItemAsync("remToken");
            ((AppAuthenticationStateProvider)_stateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public  async Task<List<ApplicationUser>> GetAllUsers()
        {
            var result = await _httpClient.GetAsync("api/Users/GetAllUsers");

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<List<ApplicationUser>>("api/Users/GetAllUsers");
        }

        public async Task<ApplicationUser> GetUserByName(string userName)
        {
            var result = await _httpClient.GetAsync($"api/Users/GetUserByName?userName={userName}");

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Users/GetUserByName?userName={userName}");
        }

        public async Task<ApplicationUser> GetUserByEmail(string userEmail)
        {
            var result = await _httpClient.GetAsync($"api/Users/GetUserByEmail?userEmail={userEmail}");

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Users/GetUserByEmail?userEmail={userEmail}");
        }

        public async Task<UsersRolesDto> GetUserWithRoles(string userId)
        {
            var result = await _httpClient.GetAsync($"api/Users/GetUserWithRoles/{userId}");

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            return await _httpClient.GetFromJsonAsync<UsersRolesDto>($"api/Users/GetUserWithRoles/{userId}");
        }

        public async Task AddUserRoles(UsersRolesDto usersRoles)
        {
            var result = await _httpClient.PostAsJsonAsync<UsersRolesDto>($"api/Users/AddUserRoles", usersRoles);

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
            await _httpClient.PostAsJsonAsync<UsersRolesDto>($"api/Users/AddUserRoles", usersRoles);
        }

        public async Task<RegistrationResponseDto> ChangePassword(ChangePasswordDto changePasswordC)
        {
            var result = await _httpClient.PutAsJsonAsync<ChangePasswordDto>
                ($"api/Users/ChangePassword?changePasswordS={changePasswordC.UserName}", changePasswordC);
            if (!result.IsSuccessStatusCode)
            {
                var resMsg = await result.Content.ReadAsStringAsync();
                var message = JsonSerializer.Deserialize<RegistrationResponseDto>(resMsg, _jsonSerializerOptions);
                return message;
            }

            return new RegistrationResponseDto { isSuccess = true };
        }

        public async Task ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            var result = await _httpClient.PostAsJsonAsync<ForgotPasswordDto>($"api/Users/ForgotPassword", forgotPassword);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
        }

        public async Task ResetPassword(ResetPasswordDto resetPassword)
        {
            var result = await _httpClient.PostAsJsonAsync<ResetPasswordDto>($"api/Users/ResetPassword", resetPassword);
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (result.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }
                var msg = await result.Content.ReadAsStringAsync();
                //throw new Exception(result.StatusCode.ToString());
                throw new Exception(result.ReasonPhrase + "_" + msg);
            }
        }
    }
}
