using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Auth;

namespace HealthGuard.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        /* User Management */
        Task CreateUserAsync(RegisterUserDTO dto);

        Task<PaginatedList<UserDTO>> GetUsersAsync(int pageIndex, int pageSize);

        Task<UserDTO> GetUserByIdAsync(string id);

        Task<UserDTO> GetUserByEmailAsync(string email);

        Task UpdateUserAsync(string id, RegisterUserDTO request);

        Task DeleteUserAsync(string id);

        /* Role Management */
        Task<List<string?>> GetRolesAsync();

        Task CreateRoleAsync(string roleName);

        Task DeleteRoleAsync(string roleName);

        /* Account Management */
        Task UpdateAccountInfoAsync(string username, UpdateUserInfoDTO dto);

        Task UpdateProfileImageAsync(string username, string imageUrl);

        Task ChangePasswordAsync(string username, ResetUserPasswordDTO dto);
    }
}
