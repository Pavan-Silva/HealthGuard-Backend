using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities.Disease;
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
        public async Task<IActionResult> GetTreatments()
        {
            return Ok(await _treatmentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreatmentById(int id)
        {
            return Ok(await _treatmentService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTreatment([FromBody] Treatment treatment)
        {
            await _treatmentService.AddAsync(treatment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatment(int id, [FromBody] Treatment treatment)
        {
            await _treatmentService.UpdateAsync(id, treatment);
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
