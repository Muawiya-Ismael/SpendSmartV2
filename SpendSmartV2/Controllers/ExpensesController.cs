using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendSmartV2.Data;
using SpendSmartV2.Interface;
using SpendSmartV2.Models;
using SpendSmartV2.Services.Expenses;

namespace SpendSmartV2.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpendSmartDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpensesServices _expensesServices;

        public List<Expenses> expensesList = new List<Expenses>();

        public ExpensesController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpendSmartDbContext context, IUnitOfWork unitOfWork, IExpensesServices expensesServices)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _unitOfWork = unitOfWork;  
            _expensesServices = expensesServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            //var allExpenses = await _context.Expenses.Where(e => e.UserId == user.Id).ToListAsync();
            //var allExpenses = await _unitOfWork.expensesRepository.GetAllAsync(user.Id);
            var allExpenses = await _expensesServices.GetAllExpenses(user.Id);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                allExpenses = allExpenses
                    .Where(e => (e.Discerption != null && e.Discerption.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                 e.Value.ToString().Contains(searchTerm))
                    .ToList();
            }

            var totalExpenses = allExpenses.Sum(e => e.Value);
            ViewBag.Expenses = totalExpenses;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ExpensesTable", allExpenses);
            }

            return View(allExpenses);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateEditExpense(int id)
        {
            if (id == null)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    //var expenseInDb = await _context.Expenses.SingleOrDefaultAsync(e => e.UserId == user.Id && e.Id == id);
                    //var expenseInDb = await _unitOfWork.expensesRepository.GetAsync(user.Id, id);
                    var expenseInDb = await _expensesServices.GetExpense(user.Id, id);

                    return View(expenseInDb ?? new Expenses());
                }

            }

        }

        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                //var expenseInDb = await _context.Expenses.SingleOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
                //var expenseInDb = await _unitOfWork.expensesRepository.GetAsync(user.Id, id);
                var expenseInDb = await _expensesServices.GetExpense(user.Id, id);

                if (expenseInDb == null)
                {
                    return NotFound();
                }

                // _context.Expenses.Remove(expenseInDb);
                //await _context.SaveChangesAsync();
                //await _unitOfWork.expensesRepository.DeleteEntity(id);
                //await _unitOfWork.CompleteAsync();
                await _expensesServices.DeleteExpense(id);
                await _expensesServices.Commit();

                return RedirectToAction("Index");
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditExpenseForm(Expenses model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                Console.WriteLine("User not found. User is null.");
                return RedirectToAction("Login", "Account");
            }

            //var existingExpense = await _context.Expenses.SingleOrDefaultAsync(e => e.Id == model.Id && e.UserId == user.Id);
            //var existingExpense = await _unitOfWork.expensesRepository.GetAsync(user.Id, model.Id);
            var existingExpense = await _expensesServices.GetExpense(user.Id, model.Id);

            if (existingExpense == null)
            {
                model.UserId = user.Id;
                //_context.Expenses.Add(model);
                //await _unitOfWork.expensesRepository.AddEntity(model);
                //await _unitOfWork.CompleteAsync();
                await _expensesServices.AddExpense(model);
                await _expensesServices.Commit();
            }
            else
            {
                //existingExpense.Discerption = model.Discerption;
                //existingExpense.Value = model.Value;

                //_context.Expenses.Update(existingExpense);
                //await _unitOfWork.expensesRepository.UpdateEntity(model);
                //await _unitOfWork.CompleteAsync();
                await _expensesServices.UpdateExpense(model);
                await _expensesServices.Commit();
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
