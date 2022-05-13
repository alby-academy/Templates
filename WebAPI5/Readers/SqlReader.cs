namespace WebAPI5.Readers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;
    using Microsoft.Extensions.Logging;

    public class SqlReader : IReader
    {
        public SqlReader(ILogger<SqlReader> logger)
        {
            logger.LogInformation("SqlReader created");
        }
        
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<IEnumerable<string>> Weathers(int days)
        {
            return Task.FromResult(Summaries.AsEnumerable());
        }
    }
}