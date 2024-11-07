using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseasesController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiseases(
            [FromQuery] DiseaseParams query,
            [FromQuery] PageParams pageParams)
        {
            return Ok(await _diseaseService.GetAllAsync(pageParams, query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiseaseById(int id)
        {
            return Ok(await _diseaseService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease([FromBody] CreateDiseaseDto model)
        {
            await _diseaseService.AddAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisease(int id, [FromBody] CreateDiseaseDto model)
        {
            await _diseaseService.UpdateAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            await _diseaseService.DeleteAsync(id);
            return Ok();
        }
    }
}
