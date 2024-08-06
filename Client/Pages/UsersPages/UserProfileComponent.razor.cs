using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.Models;

namespace News.Client.Pages.UsersPages
{
    public partial class UserProfileComponent
    {
        public ApplicationUser _user  = new ApplicationUser();

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Parameter]
        public string username { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _user = new ApplicationUser();
            _user = await _authenticationService.GetUserByName(username);
        }
    }
}
