using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Adminstration
{
    public class RolesDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage ="Role Name is Required!")]
        public string Name { get; set; }
    }
}
