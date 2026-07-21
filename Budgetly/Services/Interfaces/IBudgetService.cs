using Budgetly.Models;

namespace Budgetly.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<Budget?> GetByMonthAsync(string userId, int month, int year);
        Task<Budget?> GetByIdWithLimitsAsync(int id, string userId);
        Task<Budget> CreateAsync(Budget budget);
        Task<bool> UpdateAsync(Budget budget, string userId);
        Task<bool> DeleteAsync(int id, string userId);

        Task<BudgetLimit> AddOrUpdateLimitAsync(BudgetLimit limit);
        Task<bool> DeleteLimitAsync(int limitId, string userId);

        Task<Dictionary<int, decimal>> GetLimitsWithSpentAsync(int budgetId, string userId, int month, int year);
    }
}