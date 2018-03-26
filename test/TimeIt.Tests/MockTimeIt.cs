using TimeItCore.Timing;

namespace TimeItCore.Tests
{
    internal class MockTimeIt
    {
        private readonly IRestartableTimer _timer;

        public MockTimeIt(IRestartableTimer timer)
        {
            _timer = timer;
        }

        public Setup Then => new Setup(_timer);
    }
}