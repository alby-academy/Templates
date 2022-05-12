namespace WebAPI6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Options;

    public class WeatherForecastService : IWeatherForecastService, IWeatherForecastService2, IWeatherForecastService3
    {
        private readonly AppOptions _options;
        private readonly int _tot;
        public DateTime Time { get; }

        public WeatherForecastService(IOptions<AppOptions> options)
        {
            _options = options.Value;
            _tot = options.Value.Weathers.Count() -1;
            
            Time = DateTime.Now;
        }
        
        public IEnumerable<WeatherForecast> Get(int total)
        {
            var rng = new Random();

            return Enumerable.Range(1, total).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = _options.Weathers[_tot]
                    })
                    .ToArray();
        }
    }
}