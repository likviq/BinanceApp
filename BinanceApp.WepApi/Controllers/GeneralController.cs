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
                return NotFound();
            }

            _logger.LogInformation($"Method {nameof(GetTopCurrenciesAsync)} from {nameof(ICurrencyService)} " +
                $"returns currencies with the best volumes during the day");
            return Ok(currencies);
        }
    }
}
