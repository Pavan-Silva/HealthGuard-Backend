using HealthGuard.Application.DTOs;
using HealthGuard.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var receiver = User.Identity?.Name;
            return Ok(await _notificationService.GetAllByReceiverAsync(receiver!));
        }

        [HttpPatch("{id}")]
        public IActionResult MarkAsRead(int id)
        {
            var receiver = User.Identity?.Name;
            _notificationService.MarkAsReadAsync(id, receiver!);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var receiver = User.Identity?.Name;
            _notificationService.RemoveAsync(id, receiver!);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllByReceiver()
        {
            var receiver = User.Identity?.Name;
            _notificationService.RemoveAllByReceiverAsync(receiver!);
            return Ok();
        }
    }
}
