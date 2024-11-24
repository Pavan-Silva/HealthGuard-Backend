﻿using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface IDiseaseService
    {
        Task<PaginatedList<Disease>> GetAllAsync(PageParams pageParams, DiseaseParams query);

        Task<Disease> GetByIdAsync(int id);

        Task AddAsync(DiseaseRequest model);

        Task UpdateAsync(int id, DiseaseRequest model);

        Task DeleteAsync(int id);
    }
}
