using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ConcessionariaDuZe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role deleted successfully.");
            }

            return BadRequest(result.Errors);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRolesController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok("User added to role successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok("User removed from role successfully.");
            }

            return BadRequest(result.Errors);
        }
    }
}
