using Microsoft.Extensions.Logging;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace MontyHall.Core.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug($"Handling command {typeof(TRequest).Name}.");
            var response = await next();
            _logger.LogDebug($"Command {typeof(TRequest).Name} handled.");

            return response;
        }
    }
}
