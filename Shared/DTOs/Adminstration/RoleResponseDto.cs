using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Adminstration
{
    public class RoleResponseDto
    {
        public IEnumerable<string> Errors { get; set; }
        public bool isSuccessful { get; set; }
    }
}
