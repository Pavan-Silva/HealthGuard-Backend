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
        public async Task<IActionResult> GetSymptoms([FromQuery] int diseaseId)
        {
            return Ok(await _symptomService.GetSymptomsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptom([FromBody] string symptomName)
        {
            await _symptomService.AddSymptomAsync(symptomName);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSymptom(int id, [FromBody] string symptomName)
        {
            await _symptomService.UpdateSymptomAsync(id, symptomName);
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
