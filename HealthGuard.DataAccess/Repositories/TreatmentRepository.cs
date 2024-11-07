using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.DataAccess.Repositories
{
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
