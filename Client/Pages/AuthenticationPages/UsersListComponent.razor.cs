using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.Models;

namespace News.Client.Pages.AuthenticationPages
{
    public partial class UsersListComponent
    {
        public List<ApplicationUser> _usersList { get; set; }

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _usersList = new List<ApplicationUser>();
            _usersList =  await _authenticationService.GetAllUsers();
        }
    }
}
