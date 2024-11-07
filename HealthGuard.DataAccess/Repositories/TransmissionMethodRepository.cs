using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.DataAccess.Repositories
{
    public class TransmissionMethodRepository : Repository<TransmissionMethod>, ITransmissionMethodRepository
    {
        public TransmissionMethodRepository(AppDbContext context) : base(context)
        {
        }
    }
}
