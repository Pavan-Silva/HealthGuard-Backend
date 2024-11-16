using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Repositories.Interfaces;
using Mapster;
using System.Linq.Expressions;

namespace HealthGuard.Application.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseService(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }

        public async Task<PaginatedList<Disease>> GetAllAsync(PageParams pageParams, DiseaseParams filterParams)
        {
            var filters = new List<Expression<Func<Disease, bool>>>();

            if (!string.IsNullOrEmpty(filterParams.SearchQuery))
            {
                if (filterParams.FilterBySymptoms == true)
                {
                    filters.Add(d =>
                    d.Symptoms.Any(s => s.Name.ToLower().Contains(filterParams.SearchQuery.ToLower())));
                }
                else
                {
                    filters.Add(d =>
                    d.Name.ToLower().Contains(filterParams.SearchQuery.ToLower()));
                }
            }

            if (filterParams.TransmissionMethodId != null)
            {
                filters.Add(d =>
                d.TransmissionMethods!.Any(t => filterParams.TransmissionMethodId.Contains(t.Id)));
            }

            if (filterParams.VaccineAvailability != null)
            {
                filters.Add(d =>
                d.VaccineAvailable == filterParams.VaccineAvailability);
            }

            var result = await _diseaseRepository.GetFilteredAsync(filters,
                pageParams.PageIndex, pageParams.PageSize);

            var totalCount = await _diseaseRepository.GetFilteredCountAsync(filters);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageParams.PageSize);

            return new PaginatedList<Disease>
            (
                result.ToList(),
                pageParams.PageIndex,
                totalPages,
                totalCount
            );
        }

        public async Task<Disease> GetByIdAsync(int id)
        {
            return await _diseaseRepository.GetAsync(d => d.Id == id)
                ?? throw new NotFoundException($"Disease does not exist with id: {id}.");
        }

        public async Task AddAsync(DiseaseRequest model)
        {
            var disease = model.Adapt<Disease>();
            disease = AddDiseaseAttributesAsync(disease, model);
            disease.CreatedOn = DateTime.UtcNow;

            await _diseaseRepository.AddAsync(disease);
        }

        public async Task UpdateAsync(int id, DiseaseRequest model)
        {
            var existingDisease = await _diseaseRepository.GetAsync(d => d.Id == id)
               ?? throw new NotFoundException($"Disease does not exist with id: {id}.");

            var disease = model.Adapt(existingDisease);
            disease = AddDiseaseAttributesAsync(disease, model);
            disease.UpdatedOn = DateTime.UtcNow;

            await _diseaseRepository.UpdateAsync(disease);
        }

        public async Task DeleteAsync(int id)
        {
            var disease = await _diseaseRepository.GetAsync(d => d.Id == id)
                ?? throw new NotFoundException($"Disease does not exist with id: {id}.");

            await _diseaseRepository.RemoveAsync(disease);
        }

        private Disease AddDiseaseAttributesAsync(Disease entity, DiseaseRequest model)
        {
            if (model.Symptoms != null)
            {
                foreach (var symptomId in model.Symptoms)
                {
                    var symptom = new Symptom { Id = symptomId };
                    entity.Symptoms.Add(symptom);
                }
            }

            if (model.TransmissionMethods != null)
            {
                foreach (var transmissionMethodId in model.TransmissionMethods)
                {
                    var transmissionMethod = new TransmissionMethod { Id = transmissionMethodId };
                    entity.TransmissionMethods?.Add(transmissionMethod);
                }
            }

            if (model.Treatments != null)
            {
                foreach (var treatmentId in model.Treatments)
                {
                    var treatment = new Treatment { Id = treatmentId };
                    entity.Treatments?.Add(treatment);
                }
            }

            return entity;
        }
    }
}
