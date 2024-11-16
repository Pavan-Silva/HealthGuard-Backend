using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomService _symptomService;

        public SymptomsController(ISymptomService symptomService)
        {
            _symptomService = symptomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSymptoms(
            [FromQuery] FilterByDiseaseParams filterParams,
            [FromQuery] PageParams pageParams)
        {
            return Ok(await _symptomService.GetSymptomsAsync(filterParams, pageParams));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptom([FromBody] SymptomRequest model)
        {
            await _symptomService.AddSymptomAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSymptom(int id, [FromBody] SymptomRequest model)
        {
            await _symptomService.UpdateSymptomAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSymptom(int id)
        {
            await _symptomService.DeleteSymptomAsync(id);
            return Ok();
        }
    }
}
