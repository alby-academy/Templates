using Serilog;
using WebAPI6;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.log")
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger, true);

// Add services to the container.

builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherForecastService2, WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastService3, WeatherForecastService>();

builder.Services.AddOptions<AppOptions>().Bind(builder.Configuration.GetSection("AppOptions"));

builder.Services.AddControllers();

if(builder.Environment.IsEnvironment("Development")) Log.Logger.Information("Running in Development mode");
if(builder.Environment.IsEnvironment("Debug")) Log.Logger.Information("Running in Debug mode");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();