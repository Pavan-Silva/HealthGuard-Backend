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
    public class SymptomService : ISymptomService
    {
        private readonly ISymptomRepository _symptomRepository;

        public SymptomService(ISymptomRepository symptomRepository)
        {
            _symptomRepository = symptomRepository;
        }

        public async Task<PaginatedList<Symptom>> GetSymptomsAsync(FilterByDiseaseParams filterParams, PageParams pageParams)
        {
            Expression<Func<Symptom, bool>> filter = t => true;

            if (!string.IsNullOrEmpty(filterParams.SearchQuery))
            {
                if (filterParams.FilterByDisease == true)
                {
                    filter = t => t.Diseases.Any(d =>
                    d.Name.ToLower().Contains(filterParams.SearchQuery.ToLower()));
                }
                else
                {
                    filter = t =>
                    t.Name.ToLower().Contains(filterParams.SearchQuery.ToLower());
                }
            }

            var result = await _symptomRepository.GetAllAsync(
                filter: filter,
                orderBy: t => t.OrderBy(t => t.Id),
                includeProperties: null,
                pageParams.PageIndex,
                pageParams.PageSize);

            var totalCount = await _symptomRepository.CountAsync(filter);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageParams.PageSize);

            return new PaginatedList<Symptom>
            (
                result.ToList(),
                pageParams.PageIndex,
                totalPages,
                totalCount
            );
        }

        public async Task AddSymptomAsync(SymptomRequest model)
        {
            var symptom = model.Adapt<Symptom>();
            await _symptomRepository.AddAsync(symptom);
        }

        public async Task UpdateSymptomAsync(int id, SymptomRequest model)
        {
            var existingSymptom = await _symptomRepository.GetAsync(s => s.Id == id)
                ?? throw new NotFoundException($"Symptom does not exist with id: {id}");

            var symptom = model.Adapt(existingSymptom);
            await _symptomRepository.UpdateAsync(symptom);
        }

        public async Task DeleteSymptomAsync(int id)
        {
            var symptom = await _symptomRepository.GetAsync(s => s.Id == id)
                ?? throw new NotFoundException($"Symptom does not exist with id: {id}");

            await _symptomRepository.RemoveAsync(symptom);
        }
    }
}
