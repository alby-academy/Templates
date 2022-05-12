using WebAPI6;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherForecastService2, WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastService3, WeatherForecastService>();

builder.Services.AddOptions<AppOptions>().Bind(builder.Configuration.GetSection("AppOptions"));

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();