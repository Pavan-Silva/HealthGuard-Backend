using System.ComponentModel.DataAnnotations;

namespace HealthGuard.Application.DTOs.Auth
{
    public class ResetUserPasswordDTO
    {
        [Required]
        [MinLength(8)]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;
    }
}
