using Budgetly.Data;
using Budgetly.Models;
using Budgetly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Expenses
                .Include(e => e.BudgetCategory)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id, string userId)
        {
            return await _context.Expenses
                .Include(e => e.BudgetCategory)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> UpdateAsync(Expense expense, string userId)
        {
            var existing = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expense.Id && e.UserId == userId);

            if (existing == null)
                return false;

            existing.Name = expense.Name;
            existing.Amount = expense.Amount;
            existing.Date = expense.Date;
            existing.Description = expense.Description;
            existing.BudgetCategoryId = expense.BudgetCategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var existing = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (existing == null)
                return false;

            _context.Expenses.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalExpensesAsync(string userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }

        public async Task<decimal> GetTotalExpensesForMonthAsync(string userId, int month, int year)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;
        }

        public async Task<Dictionary<string, decimal>> GetExpensesByCategoryAsync(string userId, int month, int year)
        {
            return await _context.Expenses
                .Include(e => e.BudgetCategory)
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year)
                .GroupBy(e => e.BudgetCategory!.Name)
                .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToDictionaryAsync(x => x.Category, x => x.Total);
        }
    }
}