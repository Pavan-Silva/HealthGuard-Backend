using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.Application.Services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentService(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<IEnumerable<Treatment>> GetAllAsync()
        {
            return await _treatmentRepository.GetAllAsync();
        }

        public Task<IEnumerable<Treatment>> GetByDiseaseIdAsync(int diseaseId)
        {
            throw new NotImplementedException();
        }

        public async Task<Treatment> GetByIdAsync(int id)
        {
            return await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");
        }

        public async Task AddAsync(Treatment treatment)
        {
            await _treatmentRepository.AddAsync(treatment);
        }

        public async Task UpdateAsync(int id, Treatment treatment)
        {
            var existingTreatment = await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");

            existingTreatment.Name = treatment.Name;

            await _treatmentRepository.UpdateAsync(existingTreatment);
        }

        public async Task DeleteAsync(int id)
        {
            var treatment = await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");

            await _treatmentRepository.RemoveAsync(treatment);
        }
    }
}
