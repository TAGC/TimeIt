using System;
using System.Threading.Tasks;

namespace TimeItCore.Benchmarks
{
    public class Throw : StandardBenchmark
    {
        protected override Task ExpensiveProfiledOperation()
        {
            using (TimeIt.Then.ThrowIfLongerThan(TimeSpan.FromMilliseconds(200)))
            {
                return ExpensiveOperation();
            }
        }

        protected override Task SimpleProfiledOperation()
        {
            using (TimeIt.Then.ThrowIfLongerThan(TimeSpan.FromMilliseconds(10)))
            {
                return SimpleOperation();
            }
        }
    }
}
