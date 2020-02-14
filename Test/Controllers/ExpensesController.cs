using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Extensions;
using ExpenseTracker.Models.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;
using ExpenseTracker.Data.IRepository;
using ExpenseTracker.Utility;
using Dapper;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISP_Call _sp;

        public ExpensesController(ApplicationDbContext context, ISP_Call sp)
        {
            _context = context;
            _sp = sp;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expenses
                    .Include(e => e.Category)
                    .Include(e => e.Client)
                    .Where(e => e.UserId == User.GetUserId()).ToListAsync();

            return View(expenses);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Category)
                .Include(e => e.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            var upsertExpenseViewModel = new ExpenseViewModel
            {
                Expense = new Expense
                {
                    Date = DateTime.Now
                },
                CategoryList = _context.Categories.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                ClientList = _context.Clients.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
            };

            return View(upsertExpenseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                expense.UserId = User.GetUserId();

                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            var upsertExpenseViewModel = new ExpenseViewModel
            {
                Expense = expense,
                CategoryList = _context.Categories.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                ClientList = _context.Clients.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
            };

            return View(upsertExpenseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expense.UserId = User.GetUserId();

                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.Category)
                .Include(e => e.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

        // GET: Expenses/Create
        public IActionResult Filter()
        {
            var filterExpenseViewModel = new FilterExpenseViewModel
            {
                FilterExpense = new FilterExpense(),
                CategoryList = _context.Categories.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                ClientList = _context.Clients.Where(u => u.UserId == User.GetUserId()).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
            };

            return View(filterExpenseViewModel);
        }

        #region API Calls
        [HttpPost]
        public IActionResult Filter(FilterExpense filter)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", User.GetUserId());
            parameters.Add("@dateFrom", filter.From);
            parameters.Add("@dateTo", filter.To);
            parameters.Add("@categoryId", filter.CategoryId);
            parameters.Add("@clientId", filter.ClientId);

            var expenses = _sp.ExecuteReturnScaler<decimal>(SP.spFilterExpenses, parameters);

            return Json(expenses);
        }
        #endregion
    }
}
