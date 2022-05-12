using Microsoft.AspNetCore.Mvc;

namespace WebAPI6.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IServiceProvider provider, ILogger<WeatherForecastController> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    [HttpGet(Name = nameof(Get))]
    public IActionResult Get()
    {
        var service = _provider.GetRequiredService<IWeatherForecastService>();
        return Ok(service.Get(5));
    }
    
    [HttpGet("Get2", Name = nameof(Get2))]
    public IActionResult Get2()
    {
        var service = _provider.GetRequiredService<IWeatherForecastService2>();
        return Ok(service.Time);
    }
    
    [HttpGet("Get3", Name = nameof(Get3))]
    public IActionResult Get3()
    {
        var service1A = _provider.GetRequiredService<IWeatherForecastService>();
        Thread.Sleep(1000);
        var service1B = _provider.GetRequiredService<IWeatherForecastService>();
        
        Thread.Sleep(1000);
        var service2A = _provider.GetRequiredService<IWeatherForecastService2>();
        Thread.Sleep(1000);
        var service2B = _provider.GetRequiredService<IWeatherForecastService2>();
       
        Thread.Sleep(1000);
        var service3A = _provider.GetRequiredService<IWeatherForecastService3>();
        Thread.Sleep(1000);
        var service3B = _provider.GetRequiredService<IWeatherForecastService3>();

        var result = new[]
        {
            service1A.Time, service1B.Time, service2A.Time, service2B.Time, service3A.Time, service3B.Time
        };
        
        return Ok(result);
    }
}