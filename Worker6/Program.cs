using Worker5;
using Worker6;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", false, true);

        var config = builder.Build();
        
        services.AddOptions<AppOptions>()
            .Bind(config.GetSection("AppOptions"));

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
