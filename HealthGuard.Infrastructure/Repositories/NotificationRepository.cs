using HealthGuard.Application.Interfaces.IRepositories;
using HealthGuard.Domain.Entities;
using HealthGuard.Infrastructure.Data;

namespace HealthGuard.Infrastructure.Repositories
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
