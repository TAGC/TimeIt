using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TimeItCore.Benchmarks
{
    public class Log : StandardBenchmark
    {
        private ILogger _logger;

        [GlobalSetup]
        public void Setup()
        {
            _logger = NullLogger.Instance;
        }
        
        protected override Task ExpensiveProfiledOperation()
        {
            using (TimeIt.Then.Log(_logger, "Expensive operation completed in {Elapsed}"))
            {
                return ExpensiveOperation();
            }
        }

        protected override Task SimpleProfiledOperation()
        {
            using (TimeIt.Then.Log(_logger, "Simple operation completed in {Elapsed}"))
            {
                return SimpleOperation();
            }
        }
    }
}
