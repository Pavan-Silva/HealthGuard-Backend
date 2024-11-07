using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface ITransmissionMethodService
    {
        Task<IEnumerable<TransmissionMethod>> GetAllAsync();
    }
}
