using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<IEnumerable<Treatment>> GetAllAsync(FilterByDiseaseParams filterParams, PageParams pageParams);

        Task<Treatment> GetByIdAsync(int id);

        Task AddAsync(Treatment treatment);

        Task UpdateAsync(int id, Treatment treatment);

        Task DeleteAsync(int id);
    }
}
