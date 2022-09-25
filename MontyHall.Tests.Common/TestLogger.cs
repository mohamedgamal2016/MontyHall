using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MontyHall.Tests.Common
{
    public class TestLogger<T> : ILogger<T>
    {
        public IList<LogEntry> LogEntries { get; } = new List<LogEntry>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            LogEntries.Add(new LogEntry() { Message = message, Level = logLevel });
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
