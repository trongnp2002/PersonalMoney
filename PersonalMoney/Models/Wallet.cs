using System;
using System.Collections.Generic;
using FluentBuilder;
using System.ComponentModel.DataAnnotations;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Wallet
    {
        public Wallet()
        {
            DebtDetails = new HashSet<DebtDetail>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Balance is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Balance must be greater than 0.")]
        public decimal Balance { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DebtDetail> DebtDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
