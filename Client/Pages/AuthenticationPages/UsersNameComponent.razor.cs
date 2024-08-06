using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.Models;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class UsersNameComponent
    {
        public ApplicationUser _usersName { get; set; }

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Parameter]
        public string username { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _usersName = new ApplicationUser();
            _usersName = await _authenticationService.GetUserByName(username);
        }
    }
}
