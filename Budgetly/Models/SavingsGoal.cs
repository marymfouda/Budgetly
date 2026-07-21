using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models
{
    public class SavingsGoal
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string GoalName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TargetAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CurrentSavedAmount { get; set; } = 0;

        [Required]
        public DateTime TargetDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign Key
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}