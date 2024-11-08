using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ISymptomService
    {
        Task<IEnumerable<Symptom>> GetSymptomsAsync(SymptomParams filterParams, PageParams pageParams);

        Task AddSymptomAsync(string symptom);

        Task UpdateSymptomAsync(int id, string symptom);

        Task DeleteSymptomAsync(int id);
    }
}
