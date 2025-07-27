﻿using DomainModels.Enums;

namespace DTOs.UserDto
{
    public class UpdateUserResponseDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
    }
}
