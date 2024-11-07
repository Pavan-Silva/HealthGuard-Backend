namespace HealthGuard.Application.DTOs.Disease
{
    public class CreateDiseaseDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool VaccineAvailable { get; set; }

        public ICollection<int> Symptoms { get; set; } = [];

        public ICollection<int>? Treatments { get; set; } = [];

        public ICollection<int>? TransmissionMethods { get; set; } = [];
    }
}
