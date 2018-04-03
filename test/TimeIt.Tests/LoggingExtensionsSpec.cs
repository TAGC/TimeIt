using System;
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
                    new object[] { },
                    @"\[Debug\].*Short code region elapsed in 00:00:00\.1000000"
                };

                yield return new object[]
                {
                    1500,
                    LogLevel.Information,
                    @"Some other code region elapsed in {TimeTaken}",
                    new object[] { },
                    @"\[Information\].*Some other code region elapsed in 00:00:01\.5000000"
                };

                yield return new object[]
                {
                    45,
                    LogLevel.Trace,
                    @"It took {CodeExecutionTime} to execute this code",
                    new object[] { },
                    @"\[Trace\].*It took 00:00:00.0450000 to execute this code"
                };

                yield return new object[]
                {
                    821,
                    LogLevel.Debug,
                    @"Received {Response} from {Server} in {Elapsed}",
                    new object[] { "<data>foo</data>", "www.google.com" },
                    @"\[Debug\].*Received <data>foo<\/data> from www\.google\.com in 00:00:00\.8210000"
                };
            }
        }

        [Theory]
        [MemberData(nameof(LogExamples))]
        internal void TimeIt_Should_Be_Configurable_To_Log_Elapsed_Time(
            int elapsedTime,
            LogLevel logLevel,
            string template,
            object[] args,
            string expectedLogPattern)
        {
            var timer = ConfigureMockTimer(elapsedTime);
            var timeit = new MockTimeIt(timer);

            string log = null;
            _logger.GeneratedLog += (s, e) => log = e.Log;
            timeit.Then.Log(_logger, logLevel, template, args).Dispose();

            log.ShouldMatch(expectedLogPattern);
        }

        [Fact]
        internal void TimeIt_Should_Throw_Exception_If_Log_Template_Is_Invalid()
        {
            var timer = ConfigureMockTimer(0);
            var timeit = new MockTimeIt(timer);
            var args = new object[] { "Too", "Few", "Args" };
            var template = "{And} {Too} {Many} {Placeholders} {Elapsed}";

            Should.Throw<FormatException>(() => timeit.Then.Log(_logger, template, args).Dispose());
        }
    }
}