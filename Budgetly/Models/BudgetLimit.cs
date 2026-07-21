using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models
{
    public class BudgetLimit
    {
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal LimitAmount { get; set; }

        // Foreign Key - Budget
        public int BudgetId { get; set; }
        public Budget? Budget { get; set; }

        // Foreign Key - Category
        public int BudgetCategoryId { get; set; }
        public BudgetCategory? BudgetCategory { get; set; }
    }
}