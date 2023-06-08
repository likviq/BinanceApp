using BinanceApp.Domain.Entities;

namespace BinanceApp.Domain.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetTopCurrenciesAsync();
        Task<decimal> ConvertCurrencies(ConvertCurrency convertCurrencies);
    }
}
