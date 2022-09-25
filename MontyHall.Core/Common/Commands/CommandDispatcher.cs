using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MontyHall.Core.Common.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public CommandDispatcher(ILogger<CommandDispatcher> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<TResponse> Send<TResponse>(
            IRequest<TResponse> request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.LogDebug($"Attempting to send command of type '{typeof(IRequest<TResponse>)}'.");
            return await _mediator.Send(request);
        }
    }
}
