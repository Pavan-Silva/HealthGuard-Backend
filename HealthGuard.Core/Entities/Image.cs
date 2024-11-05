using System.ComponentModel.DataAnnotations;

namespace HealthGuard.Core.Entities
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        public required string ContentType { get; set; }

        public required byte[] Data { get; set; }
    }
}
