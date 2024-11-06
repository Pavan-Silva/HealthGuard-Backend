using HealthGuard.Application.DTOs;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthGuard.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotificationDTO>>> GetByReceiver()
        {
            var receiver = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _notificationService.GetAllByReceiverAsync(receiver!));
        }

        [HttpPatch("{id}")]
        public IActionResult MarkAsRead(Guid id)
        {
            var receiver = User.FindFirstValue(ClaimTypes.Email);
            _notificationService.MarkAsReadAsync(id, receiver!);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var receiver = User.FindFirstValue(ClaimTypes.Email);
            _notificationService.RemoveAsync(id, receiver!);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllByReceiver()
        {
            var receiver = User.FindFirstValue(ClaimTypes.Email);
            _notificationService.RemoveAllByReceiverAsync(receiver!);
            return Ok();
        }
    }
}
