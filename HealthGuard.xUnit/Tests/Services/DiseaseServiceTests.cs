using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services;
using HealthGuard.Core.Entities.Disease;
using HealthGuard.DataAccess.Repositories.Interfaces;
using Moq;

namespace HealthGuard.xUnit.Tests.Services
{
    public class DiseaseServiceTests
    {
        private readonly DiseaseService _diseaseService;
        private readonly Mock<IDiseaseRepository> _mockRepository;

        public DiseaseServiceTests()
        {
            _mockRepository = new Mock<IDiseaseRepository>();
            _diseaseService = new DiseaseService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsDisease()
        {
            var id = 1;
            var disease = new Disease
            {
                Id = id,
                Name = "Test Disease",
                VaccineAvailable = true,
                Description = "Test Description",
                Symptoms = [],
                Treatments = [],
                TransmissionMethods = []
            };

            _mockRepository.Setup(x => x.GetAsync(d => d.Id == id, null))
                .ReturnsAsync(disease);

            var result = await _diseaseService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.True(result.VaccineAvailable);
            Assert.Equal("Test Disease", result.Name);
            Assert.Equal("Test Description", result.Description);

            _mockRepository.Verify(d => d.GetAsync(d => d.Id == id, null), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsNull()
        {
            var id = 1;
            _mockRepository.Setup(x => x.GetAsync(d => d.Id == id, null))
                .ReturnsAsync((Disease?)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _diseaseService.GetByIdAsync(id));

            Assert.NotNull(exception);
            Assert.Equal($"Disease does not exist with id: {id}.", exception.Message);

            _mockRepository.Verify(d => d.GetAsync(d => d.Id == id, null), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ValidDisease_AddsDisease()
        {
        }
    }
}
