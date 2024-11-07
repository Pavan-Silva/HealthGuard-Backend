using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.DataAccess.Repositories
{
    public class SymptomRepository : Repository<Symptom>, ISymptomRepository
    {
        public SymptomRepository(AppDbContext context) : base(context)
        {
        }
    }
}
