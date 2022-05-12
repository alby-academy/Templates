using Microsoft.AspNetCore.Mvc;

namespace WebAPI6.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;
    private readonly IWeatherForecastService2 _service2;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService service, IWeatherForecastService2 service2, ILogger<WeatherForecastController> logger)
    {
        _service = service;
        _service2 = service2;
        _logger = logger;
    }

    [HttpGet(Name = nameof(Get))]
    public IActionResult Get()
    {
        return Ok(_service.Time);
    }
    
    [HttpGet("Get2", Name = nameof(Get2))]
    public IActionResult Get2()
    {
        return Ok(_service2.Time);
    }
}