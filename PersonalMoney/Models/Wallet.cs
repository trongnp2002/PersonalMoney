using System;
using System.Collections.Generic;

namespace PersonalMoney.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            DebtDetails = new HashSet<DebtDetail>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DebtDetail> DebtDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
