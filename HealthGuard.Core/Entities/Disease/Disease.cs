using HealthGuard.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace HealthGuard.Core.Entities.Disease
{
    public class Disease : IAuditedEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool VaccineAvailable { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ICollection<Symptom> Symptoms { get; set; } = [];

        public ICollection<Treatment>? Treatments { get; set; } = [];

        public ICollection<TransmissionMethod>? TransmissionMethods { get; set; } = [];

    }
}
