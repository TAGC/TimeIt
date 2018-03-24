using System;

namespace TimeItCore
{
    /// <summary>
    /// Extends <see cref="Setup" /> with useful and commonly used configuration options.
    /// </summary>
    public static class CoreExtensions
    {
        /// <summary>
        /// Configures no action to occur based on code region execution time. Effectively a pass-through.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        public static IChainableDisposable<Setup> DoNothing(this Setup setup) => setup;

        /// <summary>
        /// Configures an exception to be thrown if the code region takes longer to execute than a specified timeout.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="timeout">The maximum permitted time for the code region to execute, in milliseconds.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <exception cref="TimeoutException">
        /// The code region takes longer to execute than <paramref name="timeout"/>
        /// </exception>
        public static IChainableDisposable<Setup> ThrowIfLongerThan(this Setup setup, int timeout) =>
            setup.ThrowIfLongerThan(TimeSpan.FromMilliseconds(timeout));

        /// <summary>
        /// Configures an exception to be thrown if the code region takes longer to execute than a specified timeout.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="timeout">The maximum permitted time for the code region to execute.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <exception cref="TimeoutException">
        /// The code region takes longer to execute than <paramref name="timeout"/>
        /// </exception>
        public static IChainableDisposable<Setup> ThrowIfLongerThan(this Setup setup, TimeSpan timeout)
        {
            void Callback(TimeSpan elapsed)
            {
                if (elapsed > timeout)
                {
                    throw new TimeoutException($"Code executed in {elapsed} (exceeds timeout: {timeout}");
                }
            }

            return setup.Do(Callback);
        }
    }
}