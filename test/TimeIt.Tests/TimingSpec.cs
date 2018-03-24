using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace TimeItCore.Tests
{
    public class TimingSpec
    {
        [Theory]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(250)]
        [InlineData(500)]
        internal async Task TimeIt_Should_Accurately_Measure_Execution_Time_Of_Code_Region(int executionTime)
        {
            TimeSpan measuredTime;
            var executionTimeSpan = TimeSpan.FromMilliseconds(executionTime);

            using (TimeIt.Then.Do(elapsed => measuredTime = elapsed))
            {
                await Task.Delay(executionTimeSpan);
            }

            measuredTime.ShouldBe(executionTimeSpan, tolerance: TimeSpan.FromMilliseconds(executionTime / 5));
        }
    }
}
