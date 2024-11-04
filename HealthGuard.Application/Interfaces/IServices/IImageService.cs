using HealthGuard.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HealthGuard.Application.Interfaces.IServices
{
    public interface IImageService
    {
        Task<Image> GetImageAsync(string id);

        Task<string> SaveImageAsync(IFormFile image);

        Task UpdateImageAsync(string id, IFormFile image);

        Task DeleteImageAsync(string id);
    }
}