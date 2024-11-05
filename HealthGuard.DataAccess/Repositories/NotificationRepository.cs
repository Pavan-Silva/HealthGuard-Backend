using HealthGuard.Core.Entities;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.DataAccess.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task RemoveRangeAsync(List<Notification> notifications)
        {
            _context.RemoveRange(notifications);
            await _context.SaveChangesAsync();
        }
    }
}
