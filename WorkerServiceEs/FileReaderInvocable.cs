using Coravel.Invocable;
using System.Text;

namespace WorkerServiceEs
{
    public class FileReaderInvocable : IInvocable
    {
        private readonly AppOptions _options;
        private readonly ILogger<Worker> _logger;

        public FileReaderInvocable(AppOptions options, ILogger<Worker> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task Invoke()
        {
            var fileContent = await File.ReadAllLinesAsync(_options.InputFilePath);
            await using (var fs = File.Create(@$"{_options.OutputFilePath}\{DateTime.Now:yyyyMMdd HHmmss}.txt"))
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
                    var bytes = Encoding.UTF8.GetBytes($"{sales.VendorName} {sales.Price}");
                    await fs.WriteAsync(bytes);
                }
            }
        }
    }
}


