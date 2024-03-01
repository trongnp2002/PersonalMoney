using System;
using System.Collections.Generic;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class DebtDetail
    {
        public int Id { get; set; }
        public int? DebtorId { get; set; }
        public int? WalletId { get; set; }
        public string Note { get; set; } = null!;
        public decimal MoneyDebt { get; set; }
        public bool Classify { get; set; }
        public DateTime DateDebt { get; set; }

        public virtual Debtor? Debtor { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
