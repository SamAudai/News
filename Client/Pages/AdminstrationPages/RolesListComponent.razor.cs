using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using News.Client.Services;
using News.Shared.DTOs.Adminstration;

namespace News.Client.Pages.AdminstrationPages
{
    public partial class RolesListComponent
    {
        public List<RolesDto> _roles { get; set; }

        [Inject]
        public IMainService<RolesDto> _mainService { get; set; }

        [Inject]
        public IJSRuntime _jSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _roles = new List<RolesDto>();
            _roles = await _mainService.GetAll("api/Adminstration/GetAllRoles");
        }

        public async Task DeleteRole(string roleId)
        {
            var role = _roles.FirstOrDefault(r => r.Id == roleId);
            var confirmed = await _jSRuntime.InvokeAsync<bool>("confirm", "Delete Role?");
            if (confirmed)
            {
                await _mainService.DeleteData($"api/Adminstration/DeleteRole?RoleId={roleId}");
                _roles = await _mainService.GetAll("api/Adminstration/GetAllRoles");
            }
            
        }
    }
}
