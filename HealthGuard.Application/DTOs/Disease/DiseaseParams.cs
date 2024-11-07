namespace HealthGuard.Application.DTOs.Disease
{
    public class DiseaseParams
    {
        public bool? VaccineAvailability { get; set; }

        public string? DiseaseName { get; set; }

        public int? TreatmentId { get; set; }

        public int? SymptomId { get; set; }

        public int? TransmissionMethodId { get; set; }
    }
}
