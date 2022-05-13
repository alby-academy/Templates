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
                    /*  var results = 
                        from line in lines
                        let array = line.Split(';')
                        group array[1] by array[2] into g
                        select new { Person = g.Key, Price = g.Sum(int.Parse) };

                        var groups = lines
                        .Select(line => line.Split(','))
                        .GroupBy(line => line[0])
                        .ToDictionary(line => line[0], line => line)
                        .Select(dic => dic.Value)
                */

                    Dictionary<string, decimal> salesVendors = new Dictionary<string, decimal>();

                    List<Sales> salesList = new();
                    foreach (var line in fileContent)
                    {
                        var splitted = line.Split(';');
                        var tmp = new Sales(splitted[2], splitted[0], Convert.ToInt32(splitted[1]));
                        salesList.Add(tmp);
                    }

                    foreach (var item in salesList)
                    {
                        if (!salesVendors.ContainsKey(item.VendorName))
                        {
                            salesVendors.Add(item.VendorName, item.Price);
                        }
                        else
                        {
                            salesVendors[item.VendorName] += Convert.ToInt32(item.Price);
                        }
                    }

                    foreach (var sales in salesVendors)
                    {
                        var bytes = Encoding.UTF8.GetBytes(String.Concat($"{sales.Key} {sales.Value}", Environment.NewLine));
                        await fs.WriteAsync(bytes);
                        _logger.LogInformation(String.Concat($"{sales.Key} {sales.Value}", Environment.NewLine))
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


