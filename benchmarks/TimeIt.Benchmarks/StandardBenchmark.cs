using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace TimeItCore.Benchmarks
{
    public abstract class StandardBenchmark
    {
        private const int Iterations = 100_000;

        [Benchmark]
        public Task OneOffControl() => ExpensiveOperation();

        [Benchmark]
        public Task OneOffSut() => ExpensiveProfiledOperation();

        [Benchmark]
        public async Task LoopingControl()
        {
            for (int i=0; i < Iterations; i++)
            {
                await SimpleOperation();
            }
        }

        [Benchmark]
        public async Task LoopingSut()
        {
            for (int i=0; i < Iterations; i++)
            {
                await SimpleProfiledOperation();
            }
        }

        protected abstract Task SimpleProfiledOperation();

        protected abstract Task ExpensiveProfiledOperation();

        protected static Task SimpleOperation() => Task.FromResult(Guid.NewGuid());

        protected static Task ExpensiveOperation() => Task.Delay(100);
    }
}
