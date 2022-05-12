namespace WebAPI5
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int total);
    }
}