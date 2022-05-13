namespace WebAPI5.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;
    using Model;

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IReader _reader;

        public WeatherForecastService(IReader reader)
        {
            _reader = reader;
        }

        public async Task<IEnumerable<WeatherForecast>> Get(int total)
        {
            var rng = new Random();
            var weathers = await _reader.Weathers(total);
            var w = weathers.ToArray();

            return Enumerable.Range(1, total).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = w[rng.Next(w.Length)]
                    })
                    .ToArray();
        }
    }
}