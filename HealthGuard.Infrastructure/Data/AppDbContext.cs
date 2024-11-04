using HealthGuard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthGuard.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
    }
}
