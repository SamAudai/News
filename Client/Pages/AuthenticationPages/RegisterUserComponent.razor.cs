using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Account;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class RegisterUserComponent
    {
        public UserRegistrationDto _userRegistration = new UserRegistrationDto();

        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public bool isRegisterError { get; set; }
        public IEnumerable<string> RegisterErrors { get; set; }

        public async Task Register()
        {
            isRegisterError = false;
            var result = await _authenticationService.RegisterUser(_userRegistration);

            if (!result.isSuccess)
            {
                RegisterErrors = result.Errors;
                isRegisterError = true;
            }
            else
            {
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
