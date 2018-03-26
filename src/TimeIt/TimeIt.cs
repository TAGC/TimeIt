using JetBrains.Annotations;
using TimeItCore.Timing;

namespace TimeItCore
{
    /// <summary>
    /// The root static class for profiling regions of code and configuring actions to perform based on the time
    /// it takes for them to execute.
    /// </summary>
    [PublicAPI]
    public static class TimeIt
    {
        /// <summary>
        /// Gets an object that can be used to configure the sequence of actions to perform based on the amount of
        /// time it takes for a region of code to execute.
        /// </summary>
        /// <returns>An instance of <see cref="Setup" />.</returns>
        /// <example>
        /// <code>
        /// using (TimeIt.Then.Do(elapsed => { /* something */ }).And.Do(elapsed => { /* something else */ }))
        /// {
        ///     // Profiled code goes here.    
        /// }
        /// </code>
        /// </example>
        public static Setup Then => new Setup(new StopwatchAdapter());
    }
}