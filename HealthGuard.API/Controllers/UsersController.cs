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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return Ok(await _identityService.GetUserByIdAsync(userId));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] RegisterUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _identityService.UpdateUserAsync(userId, model);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _identityService.DeleteUserAsync(userId);
            return Ok();
        }
    }
}