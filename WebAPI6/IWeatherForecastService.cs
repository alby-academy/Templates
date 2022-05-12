namespace WebAPI6
{
    using System.Collections.Generic;

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int total);
        DateTime Time { get; }
    }
    
    public interface IWeatherForecastService2
    {
        IEnumerable<WeatherForecast> Get(int total);
        DateTime Time { get; }
    }
    
    public interface IWeatherForecastService3
    {
        IEnumerable<WeatherForecast> Get(int total);
        DateTime Time { get; }
    }
}