using Microsoft.AspNetCore.Components;
using News.Client.Authentication;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class LogoutUserComponent
    {
        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _authenticationService.LogoutUser();
            _navigationManager.NavigateTo("/");
        }
    }
}
