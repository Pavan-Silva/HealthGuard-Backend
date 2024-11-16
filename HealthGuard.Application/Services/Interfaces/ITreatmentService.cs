using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<PaginatedList<Treatment>> GetAllAsync(FilterByDiseaseParams filterParams, PageParams pageParams);

        Task<Treatment> GetByIdAsync(int id);

        Task AddAsync(TreatmentRequest model);

        Task UpdateAsync(int id, TreatmentRequest model);

        Task DeleteAsync(int id);
    }
}
