using System;

namespace TimeItCore.Timing
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    internal interface IRestartableTimer
    {
        /// <summary>
        /// Gets the time that elapsed between (re)starting this timer and stopping this timer.
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        /// Starts or restarts this timer. Elapsed time will begin being measured from this point.
        /// </summary>
        void Restart();

        /// <summary>
        /// Stops this timer. Elapsed time will be measured up to this point.
        /// </summary>
        void Stop();
    }
}