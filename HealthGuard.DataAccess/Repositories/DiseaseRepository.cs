using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Context;
using HealthGuard.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthGuard.DataAccess.Repositories
{
    public class DiseaseRepository : Repository<Disease>, IDiseaseRepository
    {
        private readonly AppDbContext _context;

        public DiseaseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Disease>> GetFilteredAsync(List<Expression<Func<Disease, bool>>> filters, int pageIndex, int pageSize)
        {
            var query = _context.Diseases.AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            query = query
                .Include(d => d.Treatments)
                .Include(d => d.TransmissionMethods)
                .Include(d => d.Symptoms)
                .AsSplitQuery();

            return await query
                .OrderBy(d => d.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetFilteredCountAsync(List<Expression<Func<Disease, bool>>> filters)
        {
            var query = _context.Diseases.AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }
    }
}
