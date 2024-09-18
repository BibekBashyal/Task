using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Inficare.Models;
using Task_Inficare.ViewModels;

namespace Task_Inficare.Controllers
{
    public class TransactionReport : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionReport(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var transactions = await _context.Transactions
                    .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                    .OrderBy(t => t.TransactionDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalTransactions = await _context.Transactions
                    .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                    .CountAsync();

                var totalPages = (int)Math.Ceiling((double)totalTransactions / pageSize);

                var model = new TransactionReportViewModel
                {
                    Transactions = transactions,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    StartDate = startDate,
                    EndDate = endDate
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Handle exception or display an error message
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
        

    }
}
