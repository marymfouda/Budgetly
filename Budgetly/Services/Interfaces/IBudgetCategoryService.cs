using Budgetly.Models;

namespace Budgetly.Services.Interfaces
{
    public interface IBudgetCategoryService
    {
        Task<List<BudgetCategory>> GetAllAsync();
        Task<BudgetCategory?> GetByIdAsync(int id);
    }
}