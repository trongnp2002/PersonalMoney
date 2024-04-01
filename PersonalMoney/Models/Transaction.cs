using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Transaction
    {
        public int? Id { get; set; }
        public string? Title { get; set; } = null!;
        [Required]
        [Range(1.0, Double.MaxValue,ErrorMessage ="{0} must be positive value")]
        [DataType(DataType.Currency, ErrorMessage ="{0} format not correct")]
        public decimal Amount { get; set; }
        public string? UserId { get; set; } = null!;
        public int? WalletId { get; set; }
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "{0} cannot be null or empty")]
        [DataType(DataType.Date, ErrorMessage = "{0} cannot be null or empty")]
        public DateTime DateOfTransaction { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
