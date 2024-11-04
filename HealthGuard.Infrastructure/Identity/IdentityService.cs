using AutoMapper;
using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Auth;
using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Interfaces.IServices;
using HealthGuard.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthGuard.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<PaginatedList<UserDTO>> GetUsersAsync(int pageIndex, int pageSize)
        {
            var users = await _userManager.Users
                .OrderBy(u => u.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await _userManager.Users.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedList<UserDTO>
            (
                _mapper.Map<List<UserDTO>>(users),
                pageIndex,
                totalPages,
                totalCount
            );
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserDTO>(user);
            dto.Roles = roles;
            return dto;
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserDTO>(user);
            dto.Roles = roles;
            return dto;
        }

        public async Task CreateUserAsync(RegisterUserDTO request)
        {
            var appUser = new User
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.FirstName + "-" + request.LastName,
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(appUser, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString()!);
        }

        public async Task UpdateUserAsync(string userId, RegisterUserDTO request)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            user.UserName = request.Email;
            user.UserName = request.FirstName + "-" + request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            if (user.Email == "admin@anima.lk")
                throw new BadRequestException("You can not delete admin user");

            await _userManager.DeleteAsync(user);
        }

        public async Task<List<string?>> GetRolesAsync()
        {
            return await _roleManager.Roles
               .Select(r => r.Name)
               .ToListAsync();
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole
            {
                Name = roleName
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString()!);
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName)
                ?? throw new NotFoundException("Role not found");

            await _roleManager.DeleteAsync(role);
        }

        public async Task UpdateAccountInfoAsync(string email, UpdateUserInfoDTO request)
        {
            var user = await _userManager.FindByEmailAsync(email)
               ?? throw new NotFoundException("User not found");

            user.UserName = request.FirstName + "-" + request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateProfileImageAsync(string email, string imageUrl)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException("User not found");

            user.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(string email, ResetUserPasswordDTO request)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException("User not found");

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString()!);
        }
    }
}
