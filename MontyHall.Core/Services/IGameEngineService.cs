using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Response;
using MontyHall.Core.Models;
using System.Threading.Tasks;

namespace MontyHall.Core.Services
{
    public interface IGameEngineService
    {
        Task<CommandResult<PayloadResponse<ScoreBoard>>> Play(PlayGameCommand command);
        
    }
}
