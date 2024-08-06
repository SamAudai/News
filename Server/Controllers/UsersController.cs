using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Shared.DTOs.Account;
using News.Shared.DTOs.Adminstration;
using News.Shared.Models;

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await _userManager.Users.ToListAsync();
            var user = usersList.Select(u => new ApplicationUser() 
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                UserRoles = string.Join(", ", _userManager.GetRolesAsync(u).Result.ToArray())
            });
            if (user == null)
            {
                return BadRequest("No Users Found");
            }
            return Ok(user);
        }

        [HttpGet]  //[HttpGet("{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                //return BadRequest("No Users Found");
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]  //[HttpGet("{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                //return BadRequest("No Users Found");
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{UserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserWithRoles(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                //return BadRequest("No Users Found");
                return NotFound();
            }
            var _roles = await _roleManager.Roles.ToListAsync();
            var _userRoles = new UsersRolesDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = _roles.Select(r => new SelectedRoleDto() 
                { 
                    Name = r.Name, 
                    isSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList()
            }; 
            return Ok(_userRoles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserRoles(UsersRolesDto userRoles)
        {
            var _user = await _userManager.FindByIdAsync(userRoles.Id);
            if(_user == null)
            {
                return NotFound();  
            }
            foreach (var item in userRoles.Roles)
            {
                if (item.isSelected)
                {
                    await _userManager.AddToRoleAsync(_user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(_user, item.Name);
                }
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordS)
        {
            var user = await _userManager.FindByNameAsync(changePasswordS.UserName);
            var newPassword = await _userManager.ChangePasswordAsync(user, changePasswordS.CurrentPassword, changePasswordS.NewPassword);
            if(user == null)
            {
                return NotFound();
            }

            if (!newPassword.Succeeded)
            {
                return Unauthorized(new RegistrationResponseDto { Errors = new[] { ("Invalid Password") } });
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto userModel)
        {
            //string email = "wassemaballan@gmail.com";
            //string subject = "Test";
            //string htmlMessage = "<h1>Welcome to my website</h1>";
            //await _emailSender.SendEmailAsync(email, subject, htmlMessage);
            //return Ok("Email is Sent");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if(user == null)
                {
                    return BadRequest("Email not found!");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = $"{userModel.Weblink}?email={userModel.Email}&token={token}";                
                await _emailSender.SendEmailAsync(userModel.Email, "Reset Password", 
                    $"<p>Hi {user.UserName},</p> <p>To Reset Your Password Please <a href={passwordResetLink}>Click Here</a> </p>");
                return Ok("Email is Sent");
            }           

            return BadRequest("Can't send email!");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if (user == null)
                {
                    return BadRequest("Email not found!");
                }
                string newToken = resetPassword.Token.Replace(" ", "+");
                var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if(!result.Succeeded) 
                {
                    return BadRequest("Can't reset password");
                }
                return Ok("Password Reset Successful!");
            }

            return BadRequest("Can't Password Reset");
        }
    }
}
