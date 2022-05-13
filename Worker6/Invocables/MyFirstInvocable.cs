using System;

namespace Worker6.Invocables;

using System.Text;
using Coravel.Invocable;
using Microsoft.Extensions.Options;
using Worker5;

public class MyFirstInvocable : IInvocable
{
    private readonly AppOptions _options;
    private readonly ILogger<Worker> _logger;

    public MyFirstInvocable(IOptions<AppOptions> options, ILogger<Worker> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    
    public async Task Invoke()
    {
        var lines = await File.ReadAllLinesAsync(@"C:\Sugar\sample\sample.txt");

        await using (var fs = File.Create(@$"C:\Sugar\sample\{DateTime.Now:yyyyMMdd HHmmss}.txt"))
        {
            foreach (var line in lines)
            {
                var bytes = Encoding.UTF8.GetBytes(line);
                await fs.WriteAsync(bytes);
            }
        }

        var message = $"{_options.Text} at: {DateTimeOffset.Now}";
        _logger.LogInformation(message);

        await Task.Delay(1000);
    }
}