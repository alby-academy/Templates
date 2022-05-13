namespace WebAPI5.Abstract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> Get(int total);
    }
}