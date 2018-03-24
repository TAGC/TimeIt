using System;
using Microsoft.Extensions.Logging;

namespace TimeItCore.Tests
{
    public class MockLogger : ILogger
    {
        public event EventHandler<GeneratedLogEventArgs> GeneratedLog;

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var log = $"[{logLevel}] {formatter(state, exception)}";

            GeneratedLog?.Invoke(this, new GeneratedLogEventArgs(log));
        }

        public class GeneratedLogEventArgs : EventArgs
        {
            public GeneratedLogEventArgs(string log)
            {
                Log = log;
            }

            public string Log { get; }
        }
    }
}