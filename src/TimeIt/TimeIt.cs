using TimeItCore.Timing;

namespace TimeItCore
{
    public static class TimeIt
    {
        public static Setup Then => new Setup(new StopwatchAdapter());
    }
}