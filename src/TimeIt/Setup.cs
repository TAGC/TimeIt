using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TimeItCore.Timing;

namespace TimeItCore
{
    /// <summary>
    /// An object that measures the time it takes for a region of code to execute and performs a configurable sequence
    /// of actions from that.
    /// </summary>
    [PublicAPI]
    public class Setup : IChainableDisposable<Setup>
    {
        private readonly List<ProcessElapsedTime> _callbacks;
        private readonly IRestartableTimer _timer;

        /// <summary>
        /// Creates a new instance of the <see cref="Setup" /> class.
        /// </summary>
        /// <param name="timer">A timer to use for measuring the elapsed time of the code region.</param>
        internal Setup(IRestartableTimer timer)
        {
            _timer = timer;
            _callbacks = new List<ProcessElapsedTime>();
        }

        /// <summary>
        /// Represents an action that performs some logic based on the time it took for a region of code to execute.
        /// </summary>
        /// <param name="elapsedTime">The elapsed execution time of the code region.</param>
        public delegate void ProcessElapsedTime(TimeSpan elapsedTime);

        /// <inheritdoc />
        public Setup And => this;

        /// <inheritdoc />
        public void Dispose()
        {
            _timer.Stop();
            _callbacks.ForEach(process => process(_timer.Elapsed));
        }

        /// <summary>
        /// Configures an action to perform based on the time it takes for the code region to execute.
        /// </summary>
        /// <param name="callback">The action to perform.</param>
        /// <returns>This instance via a chainable interface.</returns>
        /// <remarks>
        /// After the code region has finished executing, all callbacks will be invoked sequentially in the order they
        /// were configured via calls to this method.
        /// <para></para>
        /// Callbacks should be added <b>only</b> during TimeIt configuration i.e. clients should not call this method
        /// from within the code region being profiled.
        /// </remarks>
        /// <example>
        /// <code>
        /// setup.Do(elapsed => { /* some action */ });
        /// </code>
        /// </example>
        public IChainableDisposable<Setup> Do([NotNull] ProcessElapsedTime callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            _callbacks.Add(callback);
            _timer.Restart();

            return this;
        }
    }
}