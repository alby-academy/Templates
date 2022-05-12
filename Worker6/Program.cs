using Serilog;
using Worker5;
using Worker6;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.log")
            .CreateLogger();
        
        Log.Information("Hello world!");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        
        services.AddOptions<AppOptions>()
            .Bind(config.GetSection("AppOptions"));

        services.AddHostedService<Worker>();
    })
    .ConfigureLogging(builder =>
    {
        builder.AddSerilog(Log.Logger);
    })
    .Build();

await host.RunAsync();
