namespace WebAPI.Example.Models
{
    public record WeatherForecast(Location location, Current current);

    public record Location(string name, string region, string country, string localtime);

    public record Current(string lastupdated, double temp_c, double wind_kph, int humidity);
}