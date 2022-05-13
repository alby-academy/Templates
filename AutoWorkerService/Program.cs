using AutoWorkerService;
using AutoWorkerService.Invocables;
using Coravel;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.log")
            .CreateLogger();

        //Log.Information("Hello world!"); // Necessary?

        services.AddScheduler();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        services.AddOptions<AppOptions>()
            .Bind(config.GetSection("AppOptions"));

        // services.AddHostedService<Worker>();
        services.AddSingleton<EntityOperatorInvocable>();
    })
    .ConfigureLogging(builder =>
    {
        builder.AddSerilog(Log.Logger);
    })
    .Build();

host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<EntityOperatorInvocable>()
        .EveryThirtySeconds()
        .PreventOverlapping("lap1");
});

host.Run();

await host.RunAsync();

