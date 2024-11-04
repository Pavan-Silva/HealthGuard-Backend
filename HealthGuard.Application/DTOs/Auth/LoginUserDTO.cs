using System.ComponentModel.DataAnnotations;

namespace HealthGuard.Application.DTOs.Auth
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
