using System;
using Moq;
using Shouldly;
using TimeItCore.Timing;
using Xunit;
using static TimeItCore.Tests.TestUtility;

namespace TimeItCore.Tests
{
    public class CoreExtensionsSpec
    {
        [Fact]
        internal void TimeIt_Should_Be_Configurable_To_Do_Nothing()
        {
            var timeit = new MockTimeIt(Mock.Of<IRestartableTimer>());
            Should.NotThrow(() => timeit.Then.DoNothing());
        }

        [Theory]
        [InlineData(100, 150, false)]
        [InlineData(200, 200, false)]
        [InlineData(1000, 500, true)]
        internal void TimeIt_Should_Be_Configurable_To_Throw_On_Timeout(int elapsed, int timeout, bool shouldThrow)
        {
            var timer = ConfigureMockTimer(elapsed);
            var timeit = new MockTimeIt(timer);

            if (shouldThrow)
            {
                Should.Throw<TimeoutException>(() => timeit.Then.ThrowIfLongerThan(timeout).Dispose());
            }
            else
            {
                Should.NotThrow(() => timeit.Then.ThrowIfLongerThan(timeout).Dispose());
            }
        }
    }
}