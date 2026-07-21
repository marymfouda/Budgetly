using Budgetly.Models;

namespace Budgetly.Services.Interfaces
{
    public interface ISavingsGoalService
    {
        Task<List<SavingsGoal>> GetAllByUserIdAsync(string userId);
        Task<SavingsGoal?> GetByIdAsync(int id, string userId);
        Task<SavingsGoal> CreateAsync(SavingsGoal goal);
        Task<bool> UpdateAsync(SavingsGoal goal, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<decimal> GetTotalSavedAsync(string userId);
    }
}