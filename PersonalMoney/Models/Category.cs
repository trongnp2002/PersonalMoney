﻿using System;
using System.Collections.Generic;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Category
    {
        public Category()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsIncome { get; set; }
        public decimal Budget { get; set; }
        
        public DateTime? LastUpdate { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
