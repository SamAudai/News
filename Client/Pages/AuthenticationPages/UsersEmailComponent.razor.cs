using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.Models;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class UsersEmailComponent
    {
        public ApplicationUser _user { get; set; }

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Parameter]
        public string userEmail { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _user = new ApplicationUser();
            _user = await _authenticationService.GetUserByEmail(userEmail);
        }
    }
}
