using WebAPI.Example.Models;

namespace WebAPI.Example.Services.Interfaces;

public interface IWeatherForecastService
{
    Task<WeatherForecast> GetWeatherForecastForCityAsync(string cityName);
}