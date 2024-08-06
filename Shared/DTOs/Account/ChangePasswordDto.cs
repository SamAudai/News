using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Account
{
    public class ChangePasswordDto
    {
        public string UserName { get; set; }

        [Required(ErrorMessage ="Current Password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
