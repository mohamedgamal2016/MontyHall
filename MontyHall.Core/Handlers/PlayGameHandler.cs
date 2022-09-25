using MediatR;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Response;
using MontyHall.Core.Models;
using MontyHall.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MontyHall.Core.Handlers
{
    public class PlayGameHandler : IRequestHandler<PlayGameCommand, CommandResult<PayloadResponse<ScoreBoard>>>
    {
        private readonly IGameEngineService _gameEngine;
        public PlayGameHandler(IGameEngineService gameEngine)
        {
            _gameEngine = gameEngine;
        }
        public async Task<CommandResult<PayloadResponse<ScoreBoard>>> Handle(PlayGameCommand request, CancellationToken cancellationToken)
        {
            return await _gameEngine.Play(request);
        }
    }
}
