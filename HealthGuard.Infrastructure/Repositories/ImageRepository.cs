using HealthGuard.Application.Interfaces.IRepositories;
using HealthGuard.Domain.Entities;
using HealthGuard.Infrastructure.Data;

namespace HealthGuard.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {

        }
    }
}
