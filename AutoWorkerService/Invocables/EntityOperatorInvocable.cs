using System;
using System.Linq;
using System.Text;
using Coravel.Invocable;
using Microsoft.Extensions.Options;

namespace AutoWorkerService.Invocables
{
    public class EntityOperatorInvocable : IInvocable
    {
        private readonly AppOptions _appOptions;
        private readonly ILogger<Worker> _logger;

        public EntityOperatorInvocable(IOptions<AppOptions> options, ILogger<Worker> logger)
        {
            _appOptions = options.Value;
            _logger = logger;
        }

        public async Task Invoke()
        {
            List<EntityFileSupport> NameAutoPriceList = new List<EntityFileSupport>();

            var lines = await File.ReadAllLinesAsync(_appOptions.Source);

            await using (var fs = File.Create(@$"{_appOptions.Dest}\Result_Configuration_At_{DateTime.Now:yyyy-MM-dd HH-mm-ss}.txt"))
            {
                foreach (var line in lines)
                {
                    line.Trim();
                    string[] info = line.Split(";");

                    EntityFileSupport tempNameAutoPriceEntity = new EntityFileSupport
                    {
                        Name = info[0],
                        Price = int.Parse(info[2])
                    };

                    if (NameAutoPriceList.Any(o => o.Name == tempNameAutoPriceEntity.Name))
                        NameAutoPriceList.First(o => o.Name == tempNameAutoPriceEntity.Name).Price += tempNameAutoPriceEntity.Price;
                    else
                        NameAutoPriceList.Add(tempNameAutoPriceEntity);
                }

                foreach(EntityFileSupport entity in NameAutoPriceList)
                {
                    var bytes = Encoding.UTF8.GetBytes(@$"{entity}");
                    await fs.WriteAsync(bytes);

                    var message = $"{entity} at: {DateTimeOffset.Now}";
                    _logger.LogInformation(message);
                }
            }

            await Task.Delay(1000);
        }
    }
}
