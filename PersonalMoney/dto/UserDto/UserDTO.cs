using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentBuilder;

namespace PersonalMoney.dto.UserDto
{
    public class UserDTO
    {
        public class AdminResponse
        {
            public string Id { get; set; }
            public string? FirstName { get; set; } = null!;
            public string? LastName { get; set; } = null!;
            public string? AvatarUrl { get; set; } = null!;
            public string? Address { get; set; } = null!;
            public string? Email { get; set; }
            public string? LockoutEnd { get; set; }
            public bool LockoutEnabled { get; set; }
            public int AccessFailedCount { get; set; }
            public string Role { get; set; }
            public string FullName()
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}