using HealthGuard.Application.DTOs;
using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities;
using HealthGuard.DataAccess.Repositories.Interfaces;
using Mapster;

namespace HealthGuard.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationDTO>> GetAllByReceiverAsync(string receiver)
        {
            var notifications = await _notificationRepository.GetAllAsync(n => n.User.Email == receiver);
            return notifications.Adapt<List<NotificationDTO>>();
        }

        public async void MarkAsReadAsync(Guid id, string receiver)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Id == id)
                ?? throw new NotFoundException($"Couldn't find a notification with id: {id}");

            if (notification.User.Email != receiver)
                throw new BadRequestException("Invalid notification for the receiver.");

            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification);
        }

        public async void RemoveAllByReceiverAsync(string receiver)
        {
            var notifications = await _notificationRepository.GetAllAsync(n => n.User.Email == receiver);
            await _notificationRepository.RemoveRangeAsync(notifications.ToList());
        }

        public async void RemoveAsync(Guid id, string receiver)
        {
            var notification = await _notificationRepository.GetAsync(n => n.Id == id)
                ?? throw new NotFoundException($"Couldn't find a notification with id: {id}");

            if (notification.User.Email != receiver)
                throw new BadRequestException("Invalid notification for the receiver.");

            await _notificationRepository.RemoveAsync(notification);
        }

        public async void SendAsync(string message)
        {
            var notification = new Notification
            {
                User = null,
                Message = message,
            };

            await _notificationRepository.AddAsync(notification);
        }
    }
}
