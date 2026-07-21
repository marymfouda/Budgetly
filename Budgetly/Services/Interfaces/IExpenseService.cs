using Budgetly.Models;

namespace Budgetly.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllByUserIdAsync(string userId);
        Task<Expense?> GetByIdAsync(int id, string userId);
        Task<Expense> CreateAsync(Expense expense);
        Task<bool> UpdateAsync(Expense expense, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<decimal> GetTotalExpensesAsync(string userId);
        Task<decimal> GetTotalExpensesForMonthAsync(string userId, int month, int year);
        Task<Dictionary<string, decimal>> GetExpensesByCategoryAsync(string userId, int month, int year);
    }
}