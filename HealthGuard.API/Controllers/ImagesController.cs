﻿using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var image = await _imageService.GetImageAsync(id);
            return File(image.Data, image.ContentType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(Guid id, IFormFile file)
        {
            await _imageService.UpdateImageAsync(id, file);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            await _imageService.DeleteImageAsync(id);
            return Ok();
        }
    }
}
