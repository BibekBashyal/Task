namespace Task_Inficare.Services
{
    public interface IExchangeRateServices
    {
        Task<decimal> GetExchangeRateAsync(DateTime date, string fromCurrency, string toCurrency);
        Task<Dictionary<string, decimal>> GetAllRatesWithNPRAsync(DateTime date);

    }

}
