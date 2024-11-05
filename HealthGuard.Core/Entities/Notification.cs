using System.ComponentModel.DataAnnotations;

namespace HealthGuard.Core.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public required User User { get; set; }

        public required string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime Date { get; set; }
    }
}
