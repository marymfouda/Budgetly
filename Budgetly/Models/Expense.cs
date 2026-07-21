using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be more than Zero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign Key - Category
        public int BudgetCategoryId { get; set; }
        public BudgetCategory? BudgetCategory { get; set; }

        // Foreign Key - User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}