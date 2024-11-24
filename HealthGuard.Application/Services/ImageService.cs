﻿using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities;
using HealthGuard.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HealthGuard.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Image> GetImageAsync(Guid id)
        {
            return await _imageRepository.GetAsync(i => i.Id == id)
                ?? throw new NotFoundException($"Image does not exist with id: {id}");
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

        public async Task UpdateImageAsync(Guid id, IFormFile file)
        {
            var image = await _imageRepository.GetAsync(i => i.Id == id)
                ?? throw new NotFoundException($"Image does not exist with id: {id}");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            image.Data = memoryStream.ToArray();
            await _imageRepository.UpdateAsync(image);
        }

        public async Task DeleteImageAsync(Guid id)
        {
            var image = await _imageRepository.GetAsync(i => i.Id == id)
                ?? throw new NotFoundException($"Image does not exist with id: {id}");

            await _imageRepository.RemoveAsync(image);
        }
    }
}
