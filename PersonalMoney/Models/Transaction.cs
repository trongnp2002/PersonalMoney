using System;
using System.Collections.Generic;

namespace PersonalMoney.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public string UserId { get; set; } = null!;
        public int? WalletId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
