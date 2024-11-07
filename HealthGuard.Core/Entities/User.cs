using HealthGuard.Core.Common;
using Microsoft.AspNetCore.Identity;

namespace HealthGuard.Core.Entities
{
    public class User : IdentityUser, IAuditedEntity
    {
        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
