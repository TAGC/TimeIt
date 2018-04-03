using System;
using System.Linq;
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
        /// Configures a log to be generated based on the elapsed execution time of the code region. The log is generated at
        /// <see cref="LogLevel.Trace" /> level using the default log template.
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
        /// Configures a log to be generated based on the elapsed execution time of the code region. The log is generated at
        /// the specified log level using the default log template.
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
        /// Configures a log to be generated based on the elapsed execution time of the code region. The log is generated at
        /// <see cref="LogLevel.Trace" /> level using a custom template.
        /// <para></para>
        /// The log template should contain exactly one placeholder if no custom logger arguments are provided, which the elapsed
        /// time will replace. If custom arguments are provided, there should be exactly one more placeholder in the log template
        /// than the number of custom arguments, and the placeholder for the elapsed time should appear last.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <param name="template">The log template.</param>
        /// <param name="args">Additional logger arguments.</param>
        /// <returns>
        /// The <c>Setup</c> instance via a chainable interface.
        /// </returns>
        /// <example>
        /// <code>
        /// setup.Log(logger, "Connected to {Server} in {Elapsed}", serverUri);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(
            this Setup setup,
            ILogger logger,
            string template,
            params object[] args) =>
            setup.Log(logger, LogLevel.Trace, template, args);

        /// <summary>
        /// Configures a log to be generated based on the elapsed execution time of the code region. The log is generated at
        /// the specified log level using a custom template.
        /// <para></para>
        /// The log template should contain exactly one placeholder if no custom logger arguments are provided, which the elapsed
        /// time will replace. If custom arguments are provided, there should be exactly one more placeholder in the log template
        /// than the number of custom arguments, and the placeholder for the elapsed time should appear last.
        /// </summary>
        /// <param name="setup">The <c>Setup</c> instance.</param>
        /// <param name="logger">The logger to log with.</param>
        /// <param name="logLevel">The level to log at.</param>
        /// <param name="template">The log template.</param>
        /// <param name="args">Additional logger arguments.</param>
        /// <returns>The <c>Setup</c> instance via a chainable interface.</returns>
        /// <example>
        /// <code>
        /// setup.Log(logger, LogLevel.Debug, "Connected to {Server} in {Elapsed}", serverUri);
        /// </code>
        /// </example>
        public static IChainableDisposable<Setup> Log(
            this Setup setup,
            ILogger logger,
            LogLevel logLevel,
            string template,
            params object[] args)
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

            return setup.Do(elapsed => Log()(template, args.Append(elapsed).ToArray()));
        }
    }
}