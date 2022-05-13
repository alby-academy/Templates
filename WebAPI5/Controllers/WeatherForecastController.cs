using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI5.Controllers
{
    using Abstract;
    using Model;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherForecastService service, ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get(5);
            return Ok(result);
        }
        
        [HttpGet("Get2")]
        public Task<IEnumerable<WeatherForecast>> Get2()
        {
           return _service.Get(5);
        }
    }
}