using HealthGuard.Application.DTOs.Auth;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 12)
        {
            return Ok(await _identityService.GetUsersAsync(pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _identityService.GetUserByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RegisterUserDTO model)
        {
            await _identityService.UpdateUserAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _identityService.DeleteUserAsync(id);
            return Ok();
        }
    }
}