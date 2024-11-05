using Microsoft.AspNetCore.Identity;

namespace HealthGuard.Core.Entities
{
    public class User : IdentityUser
    {
        public required DateTime CreatedAt { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
