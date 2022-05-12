namespace WebAPI6
{
    using System.Collections.Generic;

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int total);
    }
}