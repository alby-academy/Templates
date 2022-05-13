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
            var lines = await File.ReadAllLinesAsync(_options.Dest);
           

            {
                
                 foreach (var line in lines)
                 {
                   var ordine = new Ordine();
                    var fileReader = line.Split(';').ToList();
                    ordine.Name = fileReader[1];
                    ordine.Model = fileReader[0];
                    ordine.Value = Int32.Parse(fileReader[2]);
                 }
                //await using (var fs = File.Create(_options.Source))
            }
        }
    }
}
