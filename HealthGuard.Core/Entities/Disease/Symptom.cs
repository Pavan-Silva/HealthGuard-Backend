using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthGuard.Core.Entities.Disease
{
    public class Symptom
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Disease> Diseases { get; set; } = [];
    }
}
