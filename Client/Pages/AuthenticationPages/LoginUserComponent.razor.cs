using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Account;
using System.Runtime.CompilerServices;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class LoginUserComponent
    {
        private readonly LoginDto _loginUser = new LoginDto();

        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Parameter]
        public bool remmemberMe { get; set; } = false;

        public bool isLoginError { get; set; }
        public IEnumerable<string> LoginErrors { get; set; }

        public async Task LoginUser()
        {
            isLoginError = false;
            _loginUser.RemmemberMe = remmemberMe;
            var result = await _authenticationService.LoginUser(_loginUser);

            if (!result.isLogin)
            {
                LoginErrors = result.Errors;
                isLoginError = true;
            }
            else
            {
                _navigationManager.NavigateTo("/");
            }
        }

        private void CheckBoxChanged()
        {
            remmemberMe = !remmemberMe;            
        }
    }
}
