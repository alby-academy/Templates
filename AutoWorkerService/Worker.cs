using Microsoft.Extensions.Options;

namespace AutoWorkerService
{
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}