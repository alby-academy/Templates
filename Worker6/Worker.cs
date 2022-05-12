namespace Worker6;

using Microsoft.Extensions.Options;
using Serilog;
using Worker5;

public class Worker : BackgroundService
{
    private readonly AppOptions _options;
    private readonly ILogger<Worker> _logger;

    public Worker(IOptions<AppOptions> options, ILogger<Worker> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = $"{_options.Text} at: {DateTimeOffset.Now}";
            _logger.LogInformation(message);
            await Task.Delay(1000, stoppingToken);
        }
    }
}