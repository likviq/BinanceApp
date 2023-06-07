using BinanceApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinanceApp.WepApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrencyService _currencyService;

        public HomeController(ILogger<HomeController> logger, ICurrencyService currencyService)
        {
            _logger = logger;
            _currencyService = currencyService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return "Hello world";
        }
    }
}