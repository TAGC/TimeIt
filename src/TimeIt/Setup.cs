using System;
using System.Collections.Generic;
using TimeItCore.Timing;

namespace TimeItCore
{
    public class Setup : IChainableDisposable<Setup>
    {
        private readonly List<ProcessElapsedTime> _callbacks;
        private readonly IRestartableTimer _timer;

        public delegate void ProcessElapsedTime(TimeSpan elapsedTime);

        public Setup And => this;

        internal Setup(IRestartableTimer timer)
        {
            _callbacks = new List<ProcessElapsedTime>();
            _timer = timer;
        }

        public IChainableDisposable<Setup> Do(ProcessElapsedTime callback)
        {
            _callbacks.Add(callback);
            _timer.Restart();

            return this;
        }

        public void Dispose()
        {
            _timer.Stop();
            _callbacks.ForEach(process => process(_timer.Elapsed));
        }
    }
}