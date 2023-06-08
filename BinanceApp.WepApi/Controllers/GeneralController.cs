using BinanceApp.Domain.Entities;
using BinanceApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BinanceApp.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly ILogger<GeneralController> _logger;
        private readonly ICurrencyService _currencyService;

        public GeneralController(ILogger<GeneralController> logger, ICurrencyService currencyService)
        {
            _logger = logger;
            _currencyService = currencyService;
        }

        [HttpGet("topcurrencies")]
        public async Task<ActionResult<List<Currency>>> GetTopCurrenciesAsync()
        {
            var currencies = await _currencyService.GetTopCurrenciesAsync();

            if (currencies == null)
            {
                _logger.LogWarning($"Method {nameof(GetTopCurrenciesAsync)} from {nameof(ICurrencyService)} returns null");
                return BadRequest();
            }

            _logger.LogInformation($"Method {nameof(GetTopCurrenciesAsync)} from {nameof(ICurrencyService)} " +
                $"returns currencies with the best volumes during the day");
            return Ok(currencies);
        }

        [HttpGet("convert")]
        public async Task<ActionResult<decimal>> GetConvertCoefficient([FromQuery] ConvertCurrency convertCurrencies)
        {
            var coef = await _currencyService.ConvertCurrencies(convertCurrencies);
            if (coef == null)
            {
                _logger.LogWarning($"Method {nameof(_currencyService.ConvertCurrencies)} from {nameof(ICurrencyService)} returns null");
                _logger.LogWarning($"One of the currency values from method {nameof(_currencyService.ConvertCurrencies)} equals to zero");
                return BadRequest();
            }

            _logger.LogInformation($"Method {nameof(GetTopCurrenciesAsync)} from {nameof(ICurrencyService)} " +
                $"returns the ratio between a currency - {convertCurrencies.CurrencyFrom} and another - {convertCurrencies.CurrencyTo}");
            return Ok(coef);
        }
    }
}
