using FluentBuilder;

namespace PersonalMoney.dto.CategoryDto
{

    [AutoGenerateBuilder]
    public class ProcessBudget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public decimal Budget { get; set; }
        public DateTime? LastUpdate { get; set; }
        public decimal Total { get; set; }
        public int? Count { get; set; } = 0;
    }

    [AutoGenerateBuilder]
    public class BudgetResponse
    {
        public List<ProcessBudget> processBudgets { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal BudgetExpense { get; set; }
    }

    public class DistributeElement
    {
        public int Id { get; set; }
        public decimal Budget { get; set;}
    }

    public class DistributeRequest
    {
        public List<DistributeElement> DistributeList { get; set; } = new List<DistributeElement>();
    }

}
