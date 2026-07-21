using Budgetly.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetLimit> BudgetLimits { get; set; }
        public DbSet<SavingsGoal> SavingsGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            builder.Entity<Income>().Property(i => i.Amount).HasPrecision(18, 2);
            builder.Entity<Expense>().Property(e => e.Amount).HasPrecision(18, 2);
            builder.Entity<Budget>().Property(b => b.MonthlyIncomeTarget).HasPrecision(18, 2);
            builder.Entity<Budget>().Property(b => b.SavingsTarget).HasPrecision(18, 2);
            builder.Entity<BudgetLimit>().Property(bl => bl.LimitAmount).HasPrecision(18, 2);
            builder.Entity<SavingsGoal>().Property(s => s.TargetAmount).HasPrecision(18, 2);
            builder.Entity<SavingsGoal>().Property(s => s.CurrentSavedAmount).HasPrecision(18, 2);

            
            builder.Entity<Expense>()
                .HasOne(e => e.BudgetCategory)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.BudgetCategoryId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<BudgetLimit>()
                .HasOne(bl => bl.Budget)
                .WithMany(b => b.BudgetLimits)
                .HasForeignKey(bl => bl.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BudgetLimit>()
                .HasOne(bl => bl.BudgetCategory)
                .WithMany(c => c.BudgetLimits)
                .HasForeignKey(bl => bl.BudgetCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.Entity<BudgetCategory>().HasData(
                new BudgetCategory { Id = 1, Name = "Food", IconClass = "bi-cup-hot" },
                new BudgetCategory { Id = 2, Name = "Shopping", IconClass = "bi-bag" },
                new BudgetCategory { Id = 3, Name = "Bills", IconClass = "bi-receipt" },
                new BudgetCategory { Id = 4, Name = "Transportation", IconClass = "bi-car-front" },
                new BudgetCategory { Id = 5, Name = "Entertainment", IconClass = "bi-controller" },
                new BudgetCategory { Id = 6, Name = "Health", IconClass = "bi-heart-pulse" },
                new BudgetCategory { Id = 7, Name = "Education", IconClass = "bi-book" },
                new BudgetCategory { Id = 8, Name = "Other", IconClass = "bi-three-dots" }
            );
        }
    }
}