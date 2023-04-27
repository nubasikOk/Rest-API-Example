using Microsoft.AspNetCore.Mvc;
using WebAPI.Example.Models;
using WebAPI.Example.Services.Interfaces;

namespace WebAPI.Example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       private readonly ILogger<WeatherForecastController> _logger;
       private readonly IWeatherForecastService _weatherForecastService;

       public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
       {
           _logger = logger;
           _weatherForecastService = weatherForecastService;
       }

        [HttpGet("city")]
        public async Task<IActionResult> GetWeatherForecastForCity([FromQuery] string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                throw new ArgumentNullException(nameof(cityName));
            }

            _logger.LogInformation("{MethodName}: Starting execution.", nameof(GetWeatherForecastForCity));
            var result = await _weatherForecastService.GetWeatherForecastForCityAsync(cityName);
           
            return Ok(result);
        }
    }
}