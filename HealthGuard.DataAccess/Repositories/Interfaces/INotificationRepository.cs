using HealthGuard.Core.Entities;

namespace HealthGuard.DataAccess.Repositories.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task RemoveRangeAsync(List<Notification> notifications);
    }
}
