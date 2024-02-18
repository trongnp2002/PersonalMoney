using System;
using System.Collections.Generic;

namespace PersonalMoney.Models
{
    public partial class Debtor
    {
        public Debtor()
        {
            DebtDetails = new HashSet<DebtDetail>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DebtDetail> DebtDetails { get; set; }
    }
}
