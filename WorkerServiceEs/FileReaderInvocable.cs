using Coravel.Invocable;
using Microsoft.Extensions.Options;
using System.Text;

namespace WorkerServiceEs
{
    public class FileReaderInvocable : IInvocable
    {
        private readonly AppOptions _options;
        private readonly ILogger<Worker> _logger;

        public FileReaderInvocable(IOptions<AppOptions> options, ILogger<Worker> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task Invoke()
        {
            try
            {
                var fileContent = await File.ReadAllLinesAsync(_options.InputFilePath);
                await using (var fs = File.Create(@$"{_options.OutputFilePath}\\{DateTime.Now:yyyyMMdd HHmmss}.txt"))
                {

                    List<Sales> salesList = new();
                    foreach (var line in fileContent)
                    {
                        var splitted = line.Split(';');
                        var tmp = new Sales
                        {
                            Product = splitted[0],
                            Price = Convert.ToInt32(splitted[1]),
                            VendorName = splitted[2],
                        };
                        salesList.Add(tmp);
                    }

                    List<Sales> result = salesList
                                .GroupBy(x => x.VendorName)
                                .Select(s => new Sales
                                {
                                    VendorName = s.First().VendorName,
                                    Product = s.First().Product,
                                    Price = s.Sum(c => c.Price),
                                }).ToList();

                    foreach (var sales in result)
                    {
                        var bytes = Encoding.UTF8.GetBytes(String.Concat($"{sales.VendorName} {sales.Price}", Environment.NewLine));

                        await fs.WriteAsync(bytes);
                    }
                }
                _logger.LogInformation(@$"Created File: {_options.OutputFilePath}\\{DateTime.Now:yyyyMMdd HHmmss}.txt");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }
    }
}


