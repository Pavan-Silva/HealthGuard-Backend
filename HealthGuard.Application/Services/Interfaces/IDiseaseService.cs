using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface IDiseaseService
    {
        Task<IEnumerable<Disease>> GetAllAsync(PageParams pageParams, DiseaseParams query);

        Task<Disease> GetByIdAsync(int id);

        Task AddAsync(CreateDiseaseDto model);

        Task UpdateAsync(int id, CreateDiseaseDto model);

        Task DeleteAsync(int id);
    }
}
