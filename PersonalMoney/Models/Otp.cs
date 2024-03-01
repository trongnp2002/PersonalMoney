using System;
using System.Collections.Generic;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Otp
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? Email { get; set; }
        public byte? Type { get; set; }
        public string Code { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public bool IsUsed { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
