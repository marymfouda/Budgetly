using Budgetly.Data;
using Budgetly.Models;
using Budgetly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Services
{
    public class BudgetCategoryService : IBudgetCategoryService
    {
        private readonly ApplicationDbContext _context;

        public BudgetCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BudgetCategory>> GetAllAsync()
        {
            return await _context.BudgetCategories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<BudgetCategory?> GetByIdAsync(int id)
        {
            return await _context.BudgetCategories
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}