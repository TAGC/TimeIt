using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit;
using static TimeItCore.Tests.TestUtility;

namespace TimeItCore.Tests
{
    public class LoggingExtensionsSpec
    {
        private readonly MockLogger _logger;

        public LoggingExtensionsSpec()
        {
            _logger = new MockLogger();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> LogExamples
        {
            get
            {
                yield return new object[]
                {
                    100,
                    LogLevel.Debug,
                    @"Short code region elapsed in {Elapsed}",
                    @"\[Debug\].*Short code region elapsed in 00:00:00\.1000000"
                };

                yield return new object[]
                {
                    1500,
                    LogLevel.Information,
                    @"Some other code region elapsed in {TimeTaken}",
                    @"\[Information\].*Some other code region elapsed in 00:00:01\.5000000"
                };

                yield return new object[]
                {
                    45,
                    LogLevel.Trace,
                    @"It took {CodeExecutionTime} to execute this code",
                    @"\[Trace\].*It took 00:00:00.0450000 to execute this code"
                };
            }
        }

        [Theory]
        [MemberData(nameof(LogExamples))]
        internal void TimeIt_Should_Be_Configurable_To_Log_Elapsed_Time(
            int elapsedTime,
            LogLevel logLevel,
            string template,
            string expectedLogPattern)
        {
            var timer = ConfigureMockTimer(elapsedTime);
            var timeit = new MockTimeIt(timer);

            string log = null;
            _logger.GeneratedLog += (s, e) => log = e.Log;
            timeit.Then.Log(_logger, logLevel, template).Dispose();

            log.ShouldMatch(expectedLogPattern);
        }
    }
}