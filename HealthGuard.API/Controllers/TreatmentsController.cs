using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentsController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTreatments(
            [FromQuery] FilterByDiseaseParams filterParams,
            [FromQuery] PageParams pageParams)
        {
            return Ok(await _treatmentService.GetAllAsync(filterParams, pageParams));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatmentById(int id)
        {
            return Ok(await _treatmentService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTreatment([FromBody] TreatmentRequest model)
        {
            await _treatmentService.AddAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatment(int id, [FromBody] TreatmentRequest model)
        {
            await _treatmentService.UpdateAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatment(int id)
        {
            await _treatmentService.DeleteAsync(id);
            return Ok();
        }
    }
}
