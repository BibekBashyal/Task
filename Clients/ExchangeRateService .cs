using Newtonsoft.Json.Linq;
using Task_Inficare.Services;

namespace Task_Inficare.Clients
{
    public class ExchangeRateService : IExchangeRateServices
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(DateTime date, string fromCurrency, string toCurrency)
        {
            var url = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={date:yyyy-MM-dd}&to={date:yyyy-MM-dd}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var payload = json["data"]?["payload"] as JArray;

            if (payload != null)
            {
                var rateData = payload.FirstOrDefault();
                if (rateData != null)
                {
                    var rates = rateData["rates"] as JArray;
                    var rateObject = rates?.FirstOrDefault(r => r["currency"]?["iso3"]?.ToString() == fromCurrency);

                    if (rateObject != null)
                    {
                        if (decimal.TryParse(rateObject["buy"]?.ToString(), out var rate))
                        {
                            return rate;
                        }
                    }
                }
            }

            throw new Exception("Exchange rate not found.");
        }
        public async Task<Dictionary<string, decimal>> GetAllRatesWithNPRAsync(DateTime date)
        {
            var url = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={date:yyyy-MM-dd}&to={date:yyyy-MM-dd}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var payload = json["data"]?["payload"] as JArray;
            var ratesWithNPR = new Dictionary<string, decimal>();

            if (payload != null)
            {
                var rateData = payload.FirstOrDefault();
                if (rateData != null)
                {
                    var rates = rateData["rates"] as JArray;
                    if (rates != null)
                    {
                        foreach (var rate in rates)
                        {
                            var currencyCode = rate["currency"]?["iso3"]?.ToString();
                            var buyRate = rate["buy"]?.ToString();

                            if (currencyCode != null && decimal.TryParse(buyRate, out var rateValue))
                            {
                                ratesWithNPR[currencyCode] = rateValue;
                            }
                        }
                    }
                }
            }

            return ratesWithNPR;
        }
    }
}
