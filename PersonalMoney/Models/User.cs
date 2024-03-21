using FluentBuilder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public class User : IdentityUser
    {
        public User()
        {
            Budgets = new HashSet<Budget>();
            Categories = new HashSet<Category>();
            Debtors = new HashSet<Debtor>();
            Otps = new HashSet<Otp>();
            Wallets = new HashSet<Wallet>();
        }
        
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? AvatarUrl { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Debtor> Debtors { get; set; }
        public virtual ICollection<Otp> Otps { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<Role> Roles { get; set; }


    }
}
