using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Account
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage ="User Name is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User Email is Required")]
        [EmailAddress(ErrorMessage = "Email format is Required")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "User Password is Required")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
