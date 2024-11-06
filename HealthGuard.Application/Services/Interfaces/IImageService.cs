using HealthGuard.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetImageAsync(Guid id);

        Task<string> SaveImageAsync(IFormFile image);

        Task UpdateImageAsync(Guid id, IFormFile image);

        Task DeleteImageAsync(Guid id);
    }
}