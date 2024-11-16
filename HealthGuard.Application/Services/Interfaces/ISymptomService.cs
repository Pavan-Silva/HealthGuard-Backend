using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ISymptomService
    {
        Task<PaginatedList<Symptom>> GetSymptomsAsync(FilterByDiseaseParams filterParams, PageParams pageParams);

        Task AddSymptomAsync(SymptomRequest model);

        Task UpdateSymptomAsync(int id, SymptomRequest model);

        Task DeleteSymptomAsync(int id);
    }
}
