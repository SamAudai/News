using Microsoft.AspNetCore.Components;
using News.Client.Services;
using News.Shared.DTOs.Adminstration;

namespace News.Client.Pages.AdminstrationPages
{
    public partial class RoleEditComponent
    {
        public RolesDto _roles = new RolesDto();

        [Inject]
        public IMainService<RolesDto> _mainService { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _roles = await _mainService.GetData($"api/Adminstration/GetRoleById/{id}");
        }

        public async Task Update()
        {
            await _mainService.UpdateData(_roles, $"api/Adminstration/EditRole?role={id}");
            _navigationManager.NavigateTo("/roleslist");
        }
    }
}
