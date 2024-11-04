using HealthGuard.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public RolesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _identityService.GetRolesAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await _identityService.CreateRoleAsync(roleName);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _identityService.DeleteRoleAsync(id);
            return Ok();
        }
    }
}
