using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Account;

namespace News.Client.Pages.UsersPages
{
    public partial class ForgotPasswordComponent
    {
        public ForgotPasswordDto _forgotPassword = new ForgotPasswordDto();

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        public async Task Send()
        {
            _forgotPassword.Weblink = _navigationManager.BaseUri + "resetpassword";
            _authenticationService.ForgotPassword(_forgotPassword);
        }
    }
}
