using HealthGuard.Application.DTOs;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetAllByReceiverAsync(string receiver);

        void SendAsync(string message);

        void MarkAsReadAsync(Guid id, string receiver);

        void RemoveAsync(Guid id, string receiver);

        void RemoveAllByReceiverAsync(string receiver);
    }
}
