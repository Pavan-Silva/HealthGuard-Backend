﻿using HealthGuard.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetImageAsync(string id);

        Task<string> SaveImageAsync(IFormFile image);

        Task UpdateImageAsync(string id, IFormFile image);

        Task DeleteImageAsync(string id);
    }
}