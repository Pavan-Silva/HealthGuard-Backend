namespace HealthGuard.Application.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }

        public required string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime Date { get; set; }

        public required string Sender { get; set; }
    }
}
