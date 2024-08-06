using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Shared.DTOs.Account;
using News.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser>? _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection  _jwtSetting;

        public AccountController(UserManager<ApplicationUser>? userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSetting = _configuration.GetSection("JwtSetting");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            if(userRegistration == null || !ModelState.IsValid)
            {
                return BadRequest("Can not Register user!");
            }

            var user = new ApplicationUser()
            {
                UserName  = userRegistration.UserName,
                Email = userRegistration.UserEmail,
            };

            var result = await _userManager.CreateAsync(user, userRegistration.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            await _userManager.AddToRoleAsync(user, "Viewer");
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UserName);
            }
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized(new LoginResponsDto { Errors = new[] { "Failed login" } }); 
            }
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateSecurityToken(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new LoginResponsDto { isLogin = true, Token = token });
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSetting["SecurityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            return claims;

        }

        private JwtSecurityToken GenerateSecurityToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSetting["ValidIssuer"],
                audience: _jwtSetting["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSetting["ExpiredTime"])),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

    }
}

