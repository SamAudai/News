using Microsoft.AspNetCore.Components;
using News.Client.Authentication;
using News.Shared.DTOs.Adminstration;

namespace News.Client.Pages.UsersPages
{
    public partial class UserRolesComponent
    {
        public UsersRolesDto _usersRoles = new UsersRolesDto();

        public List<SelectedRoleDto> _rolesList = new List<SelectedRoleDto>();

        [Inject]
        IAuthenticationService _authenticationService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string userId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _usersRoles = new UsersRolesDto();
            _usersRoles = await _authenticationService.GetUserWithRoles(userId);
        }

        public async Task Save()
        {
            await _authenticationService.AddUserRoles(_usersRoles);
            _navigationManager.NavigateTo("/userslist");
        }
    }
}
