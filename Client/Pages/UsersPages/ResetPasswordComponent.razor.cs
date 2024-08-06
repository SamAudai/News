using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Account;

namespace News.Client.Pages.UsersPages
{
    public partial class ResetPasswordComponent
    {
        public ResetPasswordDto _resetPassword = new ResetPasswordDto();

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name ="Email")]
        public string email { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "Token")]
        public string token { get; set; }

        public async Task Reset()
        {
            _resetPassword.Email = email;
            _resetPassword.Token = token;

            await _authenticationService.ResetPassword(_resetPassword);
            _navigationManager.NavigateTo("/login");
        }
    }
}
