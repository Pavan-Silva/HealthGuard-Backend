using HealthGuard.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransmissionMethodsController : ControllerBase
    {
        private readonly ITransmissionMethodService _transmissionMethodService;

        public TransmissionMethodsController(ITransmissionMethodService transmissionMethodService)
        {
            _transmissionMethodService = transmissionMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransmissionMethods()
        {
            return Ok(await _transmissionMethodService.GetAllAsync());
        }
    }
}
