using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Shared.DTOs.Adminstration;

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminstrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminstrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var roles = await _roleManager.FindByIdAsync(id);
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RolesDto role)
        {
            if(role == null || !ModelState.IsValid)
            {
                return BadRequest("Can't Add Role!");
            }
            IdentityRole identityRole = new IdentityRole() { Name = role.Name };

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RoleResponseDto { Errors = errors});
            }
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> EditRole([FromBody] RolesDto role)
        {
            var currentRole = await _roleManager.FindByIdAsync(role.Id);
            if (role == null)
            {
                return NotFound("Can't Find Role!");
            }
            currentRole.Name = role.Name;

            var result = await _roleManager.UpdateAsync(currentRole);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RoleResponseDto { Errors = errors });
            }
            return StatusCode(202);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var currentRole = await _roleManager.FindByIdAsync(roleId);
            if (roleId == null)
            {
                return NotFound("Can't Find Role!");
            }

            var result = await _roleManager.DeleteAsync(currentRole);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RoleResponseDto { Errors = errors });
            }
            return StatusCode(200);
        }
    }
}
