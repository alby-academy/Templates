using System;
using System.Text;
using Coravel.Invocable;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using MyWorker.Entities;
using System.Linq;


namespace MyWorker
{
    internal class ReadInvocables : IInvocable

    {
        private readonly MyAppOption _options;
        private readonly ILogger<Worker> _logger;

        public ReadInvocables(MyAppOption options, ILogger<Worker> logger)
        {
            _options = options;
            _logger = logger;
        }


        public async Task Invoke()
        {
            try 
            { 

            var lines = await File.ReadAllLinesAsync(_options.Dest);
                await using (var fs = File.Create(_options.Source))

                {
                    List<Ordine> ordiniTot = new();
                    foreach (var line in lines)
                    {
                        var ordine = new Ordine();
                        var fileReader = line.Split(';').ToList();
                        ordine.Name = fileReader[1];
                        ordine.Model = fileReader[0];
                        ordine.Value = Int32.Parse(fileReader[2]);
                        ordiniTot.Add(ordine);

                    }
                    List<Ordine> result = ordiniTot.GroupBy(x => x.Name).Select(x => new Ordine
                    {
                        Name = x.First().Name,
                        Model = x.First().Model,
                        Value = x.First().Value
                    })
                       .ToList();

                    foreach (var ordine in result)
                    {
                        var bytes = Encoding.UTF8.GetBytes(String.Concat($"{ordine.Name} {ordine.Value}", Environment.NewLine));
                        await fs.WriteAsync(bytes);
                    }
                    _logger.LogInformation(@$"Created File: {_options.Source}\\{DateTime.Now:yyyyMMdd HHmmss}.txt");
                    }
                

                }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}



