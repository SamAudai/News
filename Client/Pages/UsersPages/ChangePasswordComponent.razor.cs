using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Account;

namespace News.Client.Pages.UsersPages
{
    public partial class ChangePasswordComponent
    {
        public ChangePasswordDto _changePassword = new ChangePasswordDto();

        [Inject]
        IAuthenticationService _authenticationService {  get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string userName { get; set; }

        public bool changePasswordErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public async Task Save()
        {
            changePasswordErrors = false;
            _changePassword.UserName = userName;
            var result = await _authenticationService.ChangePassword(_changePassword);
            if (!result.isSuccess)
            {
                Errors = result.Errors;
                changePasswordErrors = true;
            }
            else
            {
                _authenticationService.LogoutUser();
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
