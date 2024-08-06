using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Adminstration
{
    public  class UsersRolesDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<SelectedRoleDto> Roles { get; set; }
    }
}
