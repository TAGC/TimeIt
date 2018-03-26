using System;
using System.Diagnostics;

namespace TimeItCore.Timing
{
    /// <summary>
    /// Adapts instances of <see cref="Stopwatch" /> to <see cref="IRestartableTimer" />.
    /// </summary>
    internal sealed class StopwatchAdapter : IRestartableTimer
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Creates a new instance of <see cref="StopwatchAdapter" />.
        /// </summary>
        public StopwatchAdapter()
        {
            _stopwatch = new Stopwatch();
        }

        /// <inheritdoc />
        public TimeSpan Elapsed => _stopwatch.Elapsed;

        /// <inheritdoc />
        public void Restart()
        {
            _stopwatch.Restart();
        }

        /// <inheritdoc />
        public void Stop()
        {
            _stopwatch.Stop();
        }
    }
}