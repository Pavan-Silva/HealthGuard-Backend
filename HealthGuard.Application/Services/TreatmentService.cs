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
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;

        public TreatmentService(ITreatmentRepository treatmentRepository)
        {
            _treatmentRepository = treatmentRepository;
        }

        public async Task<PaginatedList<Treatment>> GetAllAsync(FilterByDiseaseParams symptomParams, PageParams pageParams)
        {
            Expression<Func<Treatment, bool>> filter = t => true;

            if (!string.IsNullOrEmpty(symptomParams.SearchQuery))
            {
                if (symptomParams.FilterByDisease == true)
                {
                    filter = t => t.Diseases.Any(d =>
                    d.Name.ToLower().Contains(symptomParams.SearchQuery.ToLower()));
                }
                else
                {
                    filter = t =>
                    t.Name.ToLower().Contains(symptomParams.SearchQuery.ToLower());
                }
            }

            var result = await _treatmentRepository.GetAllAsync(
                filter: filter,
                orderBy: t => t.OrderBy(t => t.Id),
                includeProperties: null,
                pageParams.PageIndex,
                pageParams.PageSize);

            var totalCount = await _treatmentRepository.CountAsync(filter);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageParams.PageSize);

            return new PaginatedList<Treatment>
            (
                result.ToList(),
                pageParams.PageIndex,
                totalPages,
                totalCount
            );
        }

        public async Task<Treatment> GetByIdAsync(int id)
        {
            return await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");
        }

        public async Task AddAsync(TreatmentRequest model)
        {
            var treatment = model.Adapt<Treatment>();
            await _treatmentRepository.AddAsync(treatment);
        }

        public async Task UpdateAsync(int id, TreatmentRequest model)
        {
            var existingTreatment = await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");

            var treatment = model.Adapt(existingTreatment);
            await _treatmentRepository.UpdateAsync(treatment);
        }

        public async Task DeleteAsync(int id)
        {
            var treatment = await _treatmentRepository.GetAsync(t => t.Id == id)
                ?? throw new NotFoundException($"Treatment does not exist with id: {id}.");

            await _treatmentRepository.RemoveAsync(treatment);
        }
    }
}
