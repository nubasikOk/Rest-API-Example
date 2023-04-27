using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI.Example.Models;
using WebAPI.Example.Services.Interfaces;

namespace WebAPI.Example.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IConfiguration _configuration;

    public WeatherForecastService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<WeatherForecast> GetWeatherForecastForCityAsync(string cityName)
    {
        using var client = new HttpClient();

        client.BaseAddress = new Uri(_configuration.GetValue<string>("WeatherApiBaseURI")!);
        var path = $"v1/current.json?key={_configuration.GetValue<string>("WeatherApiKey")}&q={cityName}&aqi=no";

        var result = await client.GetAsync(path);

        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Http request failed: {result.ReasonPhrase}");
        }
        var content = await result.Content.ReadAsStreamAsync();
        try
        {
            var parsedResult = await JsonSerializer.DeserializeAsync<WeatherForecast>(content);
            return parsedResult;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}