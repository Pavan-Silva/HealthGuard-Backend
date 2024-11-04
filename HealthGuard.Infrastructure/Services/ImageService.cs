using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Interfaces.IRepositories;
using HealthGuard.Application.Interfaces.IServices;
using HealthGuard.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HealthGuard.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image> GetImageAsync(string id)
        {
            return await _imageRepository.GetAsync(i => i.Id == new Guid(id))
                ?? throw new NotFoundException("Image not found");
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var imageId = Guid.NewGuid();

            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);

            var image = new Image
            {
                Id = imageId,
                ContentType = imageFile.ContentType,
                Data = memoryStream.ToArray(),
            };

            await _imageRepository.AddAsync(image);
            return imageId.ToString();
        }

        public async Task UpdateImageAsync(string id, IFormFile file)
        {
            var image = await _imageRepository.GetAsync(i => i.Id == new Guid(id))
                ?? throw new NotFoundException("Image not found");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            image.Data = memoryStream.ToArray();
            await _imageRepository.UpdateAsync(image);
        }

        public async Task DeleteImageAsync(string id)
        {
            var image = await _imageRepository.GetAsync(i => i.Id == new Guid(id))
                ?? throw new NotFoundException("Image not found");

            await _imageRepository.RemoveAsync(image);
        }
    }
}
