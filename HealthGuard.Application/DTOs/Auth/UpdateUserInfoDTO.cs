﻿namespace HealthGuard.Application.DTOs.Auth
{
    public class UpdateUserInfoDTO
    {
        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}