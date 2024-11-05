using System.Linq.Expressions;

namespace HealthGuard.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

        Task AddAsync(T entity);

        Task RemoveAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
