using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.Application.Services
{
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomRepository _symptomRepository;

        public SymptomService(ISymptomRepository symptomRepository)
        {
            _symptomRepository = symptomRepository;
        }

        public async Task<IEnumerable<Symptom>> GetSymptomsAsync()
        {
            return await _symptomRepository.GetAllAsync();
        }

        public async Task AddSymptomAsync(string symptom)
        {
            var newSymptom = new Symptom
            {
                Name = symptom
            };

            await _symptomRepository.AddAsync(newSymptom);
        }

        public async Task UpdateSymptomAsync(int id, string symptom)
        {
            var existingSymptom = await _symptomRepository.GetAsync(s => s.Id == id)
                ?? throw new NotFoundException($"Symptom does not exist with id: {id}");

            existingSymptom.Name = symptom;
            await _symptomRepository.UpdateAsync(existingSymptom);
        }

        public async Task DeleteSymptomAsync(int id)
        {
            var symptom = await _symptomRepository.GetAsync(s => s.Id == id)
                ?? throw new NotFoundException($"Symptom does not exist with id: {id}");

            await _symptomRepository.RemoveAsync(symptom);
        }
    }
}
