using HealthGuard.Application.DTOs.Auth;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IImageService imageService,
            IConfiguration configuration,
            UserManager<User> userManager,
            IIdentityService identityService,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _imageService = imageService;
            _signInManager = signInManager;
            _configuration = configuration;
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _identityService.GetUserByEmailAsync(email!));
        }

        [Authorize]
        [HttpPost("info")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoDTO model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _identityService.UpdateAccountInfoAsync(email!, model);
            return Ok();
        }

        [Authorize]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ResetUserPasswordDTO model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _identityService.ChangePasswordAsync(email!, model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.PasswordSignInAsync(
                user!,
                dto.Password,
                isPersistent: dto.RememberMe,
                lockoutOnFailure: true);

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _identityService.CreateUserAsync(dto);
            return Ok();
        }

        [Authorize]
        [HttpPost("image")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image file provided.");
            }

            if (image.Length > long.Parse(_configuration["Uploads:MaxFileSize"]!))
            {
                return BadRequest("Image file size cannot exceed 2 MB.");
            }

            var imageId = await _imageService.SaveImageAsync(image);

            var baseUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";
            var imageUrl = baseUrl + "/api/images/" + imageId;

            var username = User.FindFirstValue(ClaimTypes.Email);
            await _identityService.UpdateProfileImageAsync(username!, imageUrl);
            return Ok();
        }
    }
}
