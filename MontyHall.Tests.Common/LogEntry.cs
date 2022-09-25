using Microsoft.Extensions.Logging;

namespace MontyHall.Tests.Common
{
    public class LogEntry
    {
        public string Message { get; set; }

        public LogLevel Level { get; set; }
    }
}
