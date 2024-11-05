using HealthGuard.Application.DTOs;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetAllByReceiverAsync(string receiver);

        void SendAsync(string message);

        void MarkAsReadAsync(int id, string receiver);

        void RemoveAsync(int id, string receiver);

        void RemoveAllByReceiverAsync(string receiver);
    }
}
