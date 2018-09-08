using System;
using System.Threading.Tasks;

namespace TimeItCore.Benchmarks
{
    public class DoNothing : StandardBenchmark
    {
        protected override Task ExpensiveProfiledOperation()
        {
            using (TimeIt.Then.DoNothing())
            {
                return ExpensiveOperation();
            }
        }

        protected override Task SimpleProfiledOperation()
        {
            using (TimeIt.Then.DoNothing())
            {
                return SimpleOperation();
            }
        }
    }
}
