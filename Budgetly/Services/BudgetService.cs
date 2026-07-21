using Budgetly.Data;
using Budgetly.Models;
using Budgetly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Budget?> GetByMonthAsync(string userId, int month, int year)
        {
            return await _context.Budgets
                .Include(b => b.BudgetLimits)
                    .ThenInclude(l => l.BudgetCategory)
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);
        }

        public async Task<Budget?> GetByIdWithLimitsAsync(int id, string userId)
        {
            return await _context.Budgets
                .Include(b => b.BudgetLimits)
                    .ThenInclude(l => l.BudgetCategory)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        public async Task<Budget> CreateAsync(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<bool> UpdateAsync(Budget budget, string userId)
        {
            var existing = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == budget.Id && b.UserId == userId);

            if (existing == null)
                return false;

            existing.MonthlyIncomeTarget = budget.MonthlyIncomeTarget;
            existing.SavingsTarget = budget.SavingsTarget;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var existing = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (existing == null)
                return false;

            _context.Budgets.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BudgetLimit> AddOrUpdateLimitAsync(BudgetLimit limit)
        {
            var existing = await _context.BudgetLimits
                .FirstOrDefaultAsync(l => l.BudgetId == limit.BudgetId && l.BudgetCategoryId == limit.BudgetCategoryId);

            if (existing != null)
            {
                existing.LimitAmount = limit.LimitAmount;
                await _context.SaveChangesAsync();
                return existing;
            }

            _context.BudgetLimits.Add(limit);
            await _context.SaveChangesAsync();
            return limit;
        }

        public async Task<bool> DeleteLimitAsync(int limitId, string userId)
        {
            var limit = await _context.BudgetLimits
                .Include(l => l.Budget)
                .FirstOrDefaultAsync(l => l.Id == limitId && l.Budget!.UserId == userId);

            if (limit == null)
                return false;

            _context.BudgetLimits.Remove(limit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<int, decimal>> GetLimitsWithSpentAsync(int budgetId, string userId, int month, int year)
        {
            
            return await _context.Expenses
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year)
                .GroupBy(e => e.BudgetCategoryId)
                .Select(g => new { CategoryId = g.Key, Total = g.Sum(e => e.Amount) })
                .ToDictionaryAsync(x => x.CategoryId, x => x.Total);
        }
    }
}