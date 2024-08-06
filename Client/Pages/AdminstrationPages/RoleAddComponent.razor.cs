using Microsoft.AspNetCore.Components;
using News.Client.Services;
using News.Shared.DTOs.Adminstration;

namespace News.Client.Pages.AdminstrationPages
{
    public partial class RoleAddComponent
    {
        public RolesDto _roles  = new RolesDto();

        [Inject]
        public IMainService<RolesDto> _mainService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public async Task Create()
        {
            await _mainService.AddData(_roles, "api/Adminstration/AddRole");
            _navigationManager.NavigateTo("/roleslist");
        }
    }
}
