using BinanceApp.Domain.Entities;
using BinanceApp.Domain.Interfaces;
using Newtonsoft.Json;
using System.Globalization;

namespace BinanceApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly decimal volumeThreshold = 1000;
        private readonly int limit = 5;
        private readonly string binanceApiTickerEndpoint = "https://api.binance.com/api/v3/ticker/24hr";
        private const string BaseUrl = "https://api.binance.com";
        private readonly HttpClient _httpClient;

        public CurrencyService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

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

        public async Task<decimal> ConvertCurrencies(ConvertCurrency convertCurrencies)
        {
            var price1 = await GetCurrencyPrice(convertCurrencies.CurrencyFrom);
            var price2 = await GetCurrencyPrice(convertCurrencies.CurrencyTo);

            if (price1 == 0 || price2 == 0)
            {
                return 0;
            }

            return price1 / price2;
        }

        private async Task<decimal> GetCurrencyPrice(string currency)
        {
            var url = $"/api/v3/ticker/price?symbol={currency}USDT";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PriceResponse>(content);

            var price = Decimal.Parse(data.Price, CultureInfo.InvariantCulture);

            return price;
        }

        private class PriceResponse
        {
            public string Price { get; set; }
        }
    }
}
