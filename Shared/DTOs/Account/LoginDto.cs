using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage ="User Name is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; } = false;
    }
}
