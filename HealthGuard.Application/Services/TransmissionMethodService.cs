using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Repositories.Interfaces;

namespace HealthGuard.Application.Services
{
    public class TransmissionMethodService : ITransmissionMethodService
    {
        private readonly ITransmissionMethodRepository _transmissionMethodRepository;

        public TransmissionMethodService(ITransmissionMethodRepository transmissionMethodRepository)
        {
            _transmissionMethodRepository = transmissionMethodRepository;
        }

        public async Task<IEnumerable<TransmissionMethod>> GetAllAsync()
        {
            return await _transmissionMethodRepository.GetAllAsync();
        }
    }
}
