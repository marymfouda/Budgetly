using System.ComponentModel.DataAnnotations;

namespace Budgetly.Models.ViewModels
{
    public class IncomeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "المصدر مطلوب")]
        [StringLength(100)]
        [Display(Name = "المصدر")]
        public string Source { get; set; } = string.Empty;

        [Required(ErrorMessage = "المبلغ مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "المبلغ لازم يكون أكبر من صفر")]
        [Display(Name = "المبلغ")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        [Display(Name = "التاريخ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(500)]
        [Display(Name = "الوصف")]
        public string? Description { get; set; }
    }
}