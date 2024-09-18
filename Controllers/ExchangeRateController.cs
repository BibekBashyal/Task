using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Task_Inficare.Services;

public class ExchangeRateController : Controller
{
    private readonly IExchangeRateServices _exchangeRateService;

    public ExchangeRateController(IExchangeRateServices exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var currentDate = DateTime.Now;
            var exchangeRates = await _exchangeRateService.GetAllRatesWithNPRAsync(currentDate);

            // Pass the rates to the view
            ViewBag.ExchangeRates = exchangeRates;
            return View();
        }
        catch (Exception ex)
        {
            // Handle exception or display an error message
            ViewBag.ErrorMessage = ex.Message;
            return View("Error");
        }
    }
}
