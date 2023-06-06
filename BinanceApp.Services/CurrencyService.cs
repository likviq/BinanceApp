using BinanceApp.Domain.Entities;
using BinanceApp.Domain.Interfaces;

namespace BinanceApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        public Task<List<Currency>> GetTopCurrencies()
        {
            throw new NotImplementedException();
        }
    }
}
