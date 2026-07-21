using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models
{
    public class BudgetCategory
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        
        [StringLength(50)]
        public string? IconClass { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<BudgetLimit> BudgetLimits { get; set; } = new List<BudgetLimit>();
    }
}