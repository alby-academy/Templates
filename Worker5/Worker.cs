using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Worker5
{
    using Microsoft.Extensions.Options;

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
}