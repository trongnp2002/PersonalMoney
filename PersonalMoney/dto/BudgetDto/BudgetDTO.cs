using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PersonalMoney.dto.BudgetDto
{
    public class BudgetDTO
    {
        public decimal? AnnuallySpending { get; set; }
        public decimal? MonthlySpending { get; set; }
        public decimal? MonthlySaving { get; set; }
        public decimal? MonthlyEarning { get; set; }
        public class Adjust
        {
            [Required]
            [NotNull]
            public decimal Spend { get; set; }

            [Required]
            [NotNull]
            public decimal Earn { get; set; }
        }
    }
}
