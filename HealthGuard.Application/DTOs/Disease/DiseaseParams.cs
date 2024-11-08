namespace HealthGuard.Application.DTOs.Disease
{
    public class DiseaseParams
    {
        public bool? VaccineAvailability { get; set; }

        public string? SearchQuery { get; set; }

        public bool? FilterBySymptoms { get; set; }

        public ICollection<int>? TransmissionMethodId { get; set; }
    }
}
