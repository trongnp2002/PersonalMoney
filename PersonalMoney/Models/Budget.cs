using System;
using System.Collections.Generic;
using FluentBuilder;

namespace PersonalMoney.Models
{
    [AutoGenerateBuilder]
    public partial class Budget
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public decimal? AnnuallySpending { get; set; }
        public decimal? MonthlySpending { get; set; }
        public decimal? MonthlySaving { get; set; }
        public decimal? MonthlyEarning { get; set; }
        public string Currency { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
