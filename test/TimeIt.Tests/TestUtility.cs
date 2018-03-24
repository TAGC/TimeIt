using System;
using Moq;
using TimeItCore.Timing;

namespace TimeItCore.Tests
{
    internal static class TestUtility
    {
        public static IRestartableTimer ConfigureMockTimer(int elapsed)
        {
            var timer = new Mock<IRestartableTimer>();
            timer.SetupGet(it => it.Elapsed).Returns(TimeSpan.FromMilliseconds(elapsed));

            return timer.Object;
        }
    }
}
