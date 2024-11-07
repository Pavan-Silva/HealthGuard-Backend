using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ITreatmentService
    {
        Task<IEnumerable<Treatment>> GetAllAsync();

        Task<IEnumerable<Treatment>> GetByDiseaseIdAsync(int id);

        Task<Treatment> GetByIdAsync(int id);

        Task AddAsync(Treatment treatment);

        Task UpdateAsync(int id, Treatment treatment);

        Task DeleteAsync(int id);
    }
}
