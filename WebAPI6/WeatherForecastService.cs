namespace WebAPI6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WeatherForecastService : IWeatherForecastService, IWeatherForecastService2, IWeatherForecastService3
    {
        public DateTime Time { get; }

        public WeatherForecastService()
        {
            Time = DateTime.Now;
        }
        
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<WeatherForecast> Get(int total)
        {
            var rng = new Random();

            return Enumerable.Range(1, total).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToArray();
        }
    }
}