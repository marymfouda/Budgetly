using Budgetly.Models;
using Budgetly.Models.ViewModels;
using Budgetly.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncomeController(IIncomeService incomeService, UserManager<ApplicationUser> userManager)
        {
            _incomeService = incomeService;
            _userManager = userManager;
        }

        private string GetUserId() => _userManager.GetUserId(User)!;

        // GET: Income
        public async Task<IActionResult> Index()
        {
            var incomes = await _incomeService.GetAllByUserIdAsync(GetUserId());
            return View(incomes);
        }

        // GET: Income/Create
        public IActionResult Create()
        {
            return View(new IncomeViewModel());
        }

        // POST: Income/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncomeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var income = new Income
            {
                Source = model.Source,
                Amount = model.Amount,
                Date = model.Date,
                Description = model.Description,
                UserId = GetUserId()
            };

            await _incomeService.CreateAsync(income);
            TempData["Success"] = "تم إضافة الدخل بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Income/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var income = await _incomeService.GetByIdAsync(id, GetUserId());
            if (income == null)
                return NotFound();

            var model = new IncomeViewModel
            {
                Id = income.Id,
                Source = income.Source,
                Amount = income.Amount,
                Date = income.Date,
                Description = income.Description
            };

            return View(model);
        }

        // POST: Income/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IncomeViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var income = new Income
            {
                Id = model.Id,
                Source = model.Source,
                Amount = model.Amount,
                Date = model.Date,
                Description = model.Description
            };

            var success = await _incomeService.UpdateAsync(income, GetUserId());
            if (!success)
                return NotFound();

            TempData["Success"] = "تم تعديل الدخل بنجاح";
            return RedirectToAction(nameof(Index));
        }

        // GET: Income/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var income = await _incomeService.GetByIdAsync(id, GetUserId());
            if (income == null)
                return NotFound();

            return View(income);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _incomeService.DeleteAsync(id, GetUserId());
            TempData["Success"] = "تم حذف الدخل بنجاح";
            return RedirectToAction(nameof(Index));
        }
    }
}