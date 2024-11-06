using HealthGuard.Core.Entities;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.DataAccess.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
