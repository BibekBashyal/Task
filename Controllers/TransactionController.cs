using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Task_Inficare.Models;
using Task_Inficare.Services;
using Task_Inficare.ViewModels;

public class TransactionController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IExchangeRateServices _exchangeRateService;

    public TransactionController(ApplicationDbContext context, IExchangeRateServices exchangeRateService)
    {
        _context = context;
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var transactionViewModel = new TransactionViewModel
        {
            ExchangeRate = await _exchangeRateService.GetExchangeRateAsync(DateTime.Now, "MYR", "NPR")
        };

        return View(transactionViewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Create(TransactionViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exchangeRate = await _exchangeRateService.GetExchangeRateAsync(DateTime.Now, "MYR", "NPR");
                model.PayoutAmountNPR = model.TransferAmountMYR * exchangeRate;

                var transaction = new Transaction
                {
                    SenderFirstName = model.SenderFirstName,
                    SenderMiddleName = model.SenderMiddleName,
                    SenderLastName = model.SenderLastName,
                    SenderAddress = model.SenderAddress,
                    SenderCountry = model.SenderCountry,
                    ReceiverFirstName = model.ReceiverFirstName,
                    ReceiverMiddleName = model.ReceiverMiddleName,
                    ReceiverLastName = model.ReceiverLastName,
                    ReceiverAddress = model.ReceiverAddress,
                    ReceiverCountry = model.ReceiverCountry,
                    ReceiverBankName = model.ReceiverBankName,
                    ReceiverAccountNumber = model.ReceiverAccountNumber,
                    TransferAmountMYR = model.TransferAmountMYR,
                    ExchangeRate = exchangeRate,
                    PayoutAmountNPR = model.PayoutAmountNPR,
                    TransactionDate = DateTime.Now
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToAction("GenerateReport","TransactionReport");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(model);
    }
}
