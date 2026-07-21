using Budgetly.Data;
using Budgetly.Models;
using Budgetly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly ApplicationDbContext _context;

        public IncomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id, string userId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        }

        public async Task<Income> CreateAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task<bool> UpdateAsync(Income income, string userId)
        {
            var existing = await GetByIdAsync(income.Id, userId);
            if (existing == null)
                return false;

            existing.Source = income.Source;
            existing.Amount = income.Amount;
            existing.Date = income.Date;
            existing.Description = income.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var existing = await GetByIdAsync(id, userId);
            if (existing == null)
                return false;

            _context.Incomes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalIncomeAsync(string userId)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId)
                .SumAsync(i => (decimal?)i.Amount) ?? 0;
        }

        public async Task<decimal> GetTotalIncomeForMonthAsync(string userId, int month, int year)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId && i.Date.Month == month && i.Date.Year == year)
                .SumAsync(i => (decimal?)i.Amount) ?? 0;
        }
    }
}