using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Debtor
    {
        public Debtor()
        {
            DebtDetails = new HashSet<DebtDetail>();
        }

        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        [Required(ErrorMessage = "Name not empty")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        [Phone(ErrorMessage = "Phone is invalid")]
        [Display(Name = "Phone")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email not empty")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DebtDetail> DebtDetails { get; set; }
    }
}
