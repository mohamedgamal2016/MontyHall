using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Response;
using MontyHall.Core.Models;
using MediatR;

namespace MontyHall.Core.Commands
{
    public class PlayGameCommand : IRequest<CommandResult<PayloadResponse<ScoreBoard>>>
    {
        public int Tries { get; set; }

        public bool IsSwitchStrategyEnabled { get; set; }
    }
}
