using Coravel;
using Serilog;
using Worker5;
using Worker6;
using Worker6.Invocables;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.log")
            .CreateLogger();
        
        Log.Information("Hello world!");
        
        services.AddScheduler();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        services.AddOptions<AppOptions>()
            .Bind(config.GetSection("AppOptions"));
        
        // services.AddHostedService<Worker>();
        services.AddSingleton<MyFirstInvocable>();
    })
    .ConfigureLogging(builder =>
    {
        builder.AddSerilog(Log.Logger);
    })
    .Build();

host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<MyFirstInvocable>()
        .EveryFiveSeconds()
        .PreventOverlapping("lap1");
});

host.Run();

await host.RunAsync();
