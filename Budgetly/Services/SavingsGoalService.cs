using Budgetly.Data;
using Budgetly.Models;
using Budgetly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Services
{
    public class SavingsGoalService : ISavingsGoalService
    {
        private readonly ApplicationDbContext _context;

        public SavingsGoalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SavingsGoal>> GetAllByUserIdAsync(string userId)
        {
            return await _context.SavingsGoals
                .Where(g => g.UserId == userId)
                .OrderBy(g => g.TargetDate)
                .ToListAsync();
        }

        public async Task<SavingsGoal?> GetByIdAsync(int id, string userId)
        {
            return await _context.SavingsGoals
                .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
        }

        public async Task<SavingsGoal> CreateAsync(SavingsGoal goal)
        {
            _context.SavingsGoals.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<bool> UpdateAsync(SavingsGoal goal, string userId)
        {
            var existing = await GetByIdAsync(goal.Id, userId);
            if (existing == null)
                return false;

            existing.GoalName = goal.GoalName;
            existing.TargetAmount = goal.TargetAmount;
            existing.CurrentSavedAmount = goal.CurrentSavedAmount;
            existing.TargetDate = goal.TargetDate;
            existing.Description = goal.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var existing = await GetByIdAsync(id, userId);
            if (existing == null)
                return false;

            _context.SavingsGoals.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSavedAsync(string userId)
        {
            return await _context.SavingsGoals
                .Where(g => g.UserId == userId)
                .SumAsync(g => (decimal?)g.CurrentSavedAmount) ?? 0;
        }
    }
}