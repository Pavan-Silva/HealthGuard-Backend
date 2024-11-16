namespace HealthGuard.Application.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime? LastLogin { get; set; }

        public IEnumerable<string>? Roles { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool Disabled { get; set; }
    }
}
