using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ISymptomService
    {
        Task<IEnumerable<Symptom>> GetSymptomsAsync();

        Task AddSymptomAsync(string symptom);

        Task UpdateSymptomAsync(int id, string symptom);

        Task DeleteSymptomAsync(int id);
    }
}
