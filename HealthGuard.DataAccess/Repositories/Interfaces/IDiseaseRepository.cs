using HealthGuard.Core.Entities.Disease;
using System.Linq.Expressions;

namespace HealthGuard.DataAccess.Repositories.Interfaces
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<IEnumerable<Disease>> GetFilteredAsync(List<Expression<Func<Disease, bool>>> filters, int pageIndex, int pageSize);

        Task<int> GetFilteredCountAsync(List<Expression<Func<Disease, bool>>> filters);
    }
}
