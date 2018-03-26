using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace TimeItCore
{
    /// <summary>
    /// Extends <see cref="Setup" /> with logging-related configuration options.
    /// </summary>
    [PublicAPI]
    public static class LoggingExtensions
    {
        private const string DefaultLogTemplate = "Code region executed in {Elapsed}";

        /// <summary>
        /// Configures a log to be generated based on the elapsed execution time of the code region at
        /// <see cref="LogLevel.Trace" /> using the default log template.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <example>
        /// <code>
        /// setup.Log(logger);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(this Setup setup, ILogger logger) =>
            setup.Log(logger, LogLevel.Trace, DefaultLogTemplate);

        /// <summary>
        /// Configures a log to be generated based on the elapsed execution time of the code region at the specified
        /// log level using the default log template.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <param name="logLevel">The level to log at.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <example>
        /// <code>
        /// setup.Log(logger, LogLevel.Debug);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(this Setup setup, ILogger logger, LogLevel logLevel) =>
            setup.Log(logger, logLevel, DefaultLogTemplate);

        /// <summary>
        /// Configures a log to be generated based on the elapsed execution time of the code region at
        /// <see cref="LogLevel.Trace" /> using a custom template.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <param name="template">
        /// The log template. This should contain exactly one placeholder, which the elapsed time will replace.
        /// </param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <example>
        /// <code>
        /// setup.Log(logger, "Code region executed in {Elapsed}");
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(this Setup setup, ILogger logger, string template) =>
            setup.Log(logger, LogLevel.Trace, template);

        /// <summary>
        /// Configures a log to be generated based on the elapsed execution time of the code region at the specified
        /// log level using a custom template.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <param name="logLevel">The level to log at.</param>
        /// <param name="template">
        /// The log template. This should contain exactly one placeholder, which the elapsed time will replace.
        /// </param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <example>
        /// <code>
        /// setup.Log(logger, LogLevel.Debug, "Code region executed in {Elapsed}");
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(
            this Setup setup,
            ILogger logger,
            LogLevel logLevel,
            string template)
        {
            if (logLevel == LogLevel.None)
            {
                return setup;
            }

            Action<string, object[]> Log()
            {
                switch (logLevel)
                {
                    case LogLevel.Trace: return logger.LogTrace;
                    case LogLevel.Debug: return logger.LogDebug;
                    case LogLevel.Information: return logger.LogInformation;
                    case LogLevel.Warning: return logger.LogWarning;
                    case LogLevel.Error: return logger.LogError;
                    case LogLevel.Critical: return logger.LogCritical;
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            return setup.Do(elapsed => Log()(template, new object[] { elapsed }));
        }
    }
}