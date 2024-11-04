using Microsoft.AspNetCore.Identity;

namespace HealthGuard.Domain.Entities
{
    public class User : IdentityUser
    {
        public required DateTime CreatedAt { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
