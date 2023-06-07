using BinanceApp.Domain.Entities;
using BinanceApp.Domain.Interfaces;
using Newtonsoft.Json;

namespace BinanceApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly decimal volumeThreshold = 1000;
        private readonly int limit = 5;
        private readonly string binanceApiTickerEndpoint = "https://api.binance.com/api/v3/ticker/24hr";

        public async Task<List<Currency>> GetTopCurrenciesAsync()
        {
            string url = binanceApiTickerEndpoint;
            List<Currency> currencies = new List<Currency>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                currencies = JsonConvert.DeserializeObject<List<Currency>>(responseBody);
            }

            // Відфільтрувати валюти за обсягом торгів
            currencies = currencies.Where(c => c.Volume >= volumeThreshold).ToList();

            // Вибрати перші 'limit' валют
            currencies = currencies.OrderByDescending(c => c.Volume).Take(limit).ToList();

            return currencies;
        }
    }
}
