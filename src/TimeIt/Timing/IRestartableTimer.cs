using System;

namespace TimeItCore.Timing
{
    internal interface IRestartableTimer
    {
        TimeSpan Elapsed { get; }

        void Restart();

        void Stop();
    }
}