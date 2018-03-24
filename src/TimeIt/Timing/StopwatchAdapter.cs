using System;
using System.Diagnostics;

namespace TimeItCore.Timing
{
    internal sealed class StopwatchAdapter : IRestartableTimer
    {
        private readonly Stopwatch _stopwatch;

        public StopwatchAdapter()
        {
            _stopwatch = new Stopwatch();
        }

        public TimeSpan Elapsed => _stopwatch.Elapsed;

        public void Restart()
        {
            _stopwatch.Restart();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}