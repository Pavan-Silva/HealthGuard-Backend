using HealthGuard.Domain.Entities;

namespace HealthGuard.Application.Interfaces.IRepositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task RemoveRangeAsync(List<Notification> notifications);
    }
}
