using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Worker5
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.json", false, true);

                    var config = builder.Build();

                    services
                        .AddOptions<AppOptions>()
                        .Bind(config.GetSection("AppOptions"));
                    
                    services.AddHostedService<Worker>();
                })
                
                .Build()
                .RunAsync();
        }
    }
}