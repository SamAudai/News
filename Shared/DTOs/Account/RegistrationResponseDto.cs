using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Account
{
    public class RegistrationResponseDto
    {
        public bool isSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
