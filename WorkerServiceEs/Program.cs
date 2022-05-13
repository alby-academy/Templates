using Serilog;
using Coravel;
using WorkerServiceEs;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("log.log")
        .CreateLogger();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        services.AddOptions<AppOptions>()
            .Bind(config.GetSection("AppOptions"));

        services.AddSingleton<FileReaderInvocable>();
        services.AddScheduler();
    })
    .ConfigureLogging(builder =>
    {
        builder.AddSerilog(Log.Logger);
    })
    .Build();

host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<FileReaderInvocable>()
        .EveryThirtySeconds()
        .PreventOverlapping("lap1");
});

await host.RunAsync();
