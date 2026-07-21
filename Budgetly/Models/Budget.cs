using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models
{
    public class Budget
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MonthlyIncomeTarget { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SavingsTarget { get; set; }

        // Foreign Key
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public ICollection<BudgetLimit> BudgetLimits { get; set; } = new List<BudgetLimit>();
    }
}