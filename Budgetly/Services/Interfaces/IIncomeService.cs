using Budgetly.Models;

namespace Budgetly.Services.Interfaces
{
    public interface IIncomeService
    {
        Task<List<Income>> GetAllByUserIdAsync(string userId);
        Task<Income?> GetByIdAsync(int id, string userId);
        Task<Income> CreateAsync(Income income);
        Task<bool> UpdateAsync(Income income, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<decimal> GetTotalIncomeAsync(string userId);
        Task<decimal> GetTotalIncomeForMonthAsync(string userId, int month, int year);
    }
}