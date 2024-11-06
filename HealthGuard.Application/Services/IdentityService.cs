using AutoMapper;
using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Auth;
using HealthGuard.Application.Exceptions;
using HealthGuard.Application.Services.Interfaces;
using HealthGuard.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthGuard.Application.Services
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
                ?? throw new NotFoundException($"User does not exist with email: {email}");

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserDTO>(user);
            dto.Roles = roles;
            return dto;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new NotFoundException($"Couldn't find user with Id: {id}");

            var roles = await _userManager.GetRolesAsync(user);
            var dto = _mapper.Map<UserDTO>(user);
            dto.Roles = roles;
            return dto;
        }

        public async Task CreateUserAsync(RegisterUserDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.FirstName + "-" + dto.LastName,
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.First().ToString()!);
        }

        public async Task UpdateUserAsync(string id, RegisterUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new NotFoundException($"Couldn't find user with Id: {id}");

            user.UserName = dto.Email;
            user.UserName = dto.FirstName + "-" + dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new NotFoundException($"Couldn't find user with Id: {id}");

            if (user.Email == "admin@healthguard.lk")
                throw new BadRequestException("Admin user cannot be deleted.");

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
                ?? throw new NotFoundException($"Role does not exist with name: {roleName}");

            await _roleManager.DeleteAsync(role);
        }

        public async Task UpdateAccountInfoAsync(string email, UpdateUserInfoDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(email)
               ?? throw new NotFoundException($"User does not exist with email: {email}");

            user.UserName = dto.FirstName + "-" + dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateProfileImageAsync(string email, string imageUrl)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException($"User does not exist with email: {email}");

            user.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(string email, ResetUserPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException($"User does not exist with email: {email}");

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.ToString()!);
        }
    }
}
