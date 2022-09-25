using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Extensions;
using MontyHall.Core.Common.Extensions.Response;
using MontyHall.Core.Factories;
using MontyHall.Core.Models;

namespace MontyHall.Core.Services
{
    public class GameEngine : IGameEngine
    {
        private readonly IDoorFactory _doorFactory;
        private readonly IScoreBoardFactory _scoreBoardFactory;
        private readonly ILogger<GameEngine> _logger;

        private int[] DOOR_INDEXES = new[] { 0, 1, 2 };
        private List<Door> Doors { get; set; }


        public GameEngine(IDoorFactory doorFactory, IScoreBoardFactory scoreBoardFactory, ILogger<GameEngine> logger)
        {
            _doorFactory = doorFactory;
            _scoreBoardFactory = scoreBoardFactory;
            _logger = logger;
        }
        protected Random Random { get; set; } = new Random();

        public async Task<CommandResult<PayloadResponse<ScoreBoard>>> Play(PlayGameCommand command)
        {
            try
            {
                Doors = _doorFactory.Create().ToList();
                int wins = 0, loss = 0;

                for (int i = 0; i < command.Tries; i++)
                {
                    Random.Shuffle(Doors);
                    int choiceIndex = Random.Next(0, 3);
                    int carIndex = Doors.IndexOf(Doors.SingleOrDefault(door => door.IsPrize));

                    if (command.IsSwitchStrategyEnabled)
                    {
                        int doorToRevealIndex;
                        while (true)
                        {
                            doorToRevealIndex = Random.Next(0, 3);
                            if (doorToRevealIndex != choiceIndex && !Doors[doorToRevealIndex].IsPrize)
                                break;
                        }
                        choiceIndex = DOOR_INDEXES.SingleOrDefault(index => index != doorToRevealIndex && index != choiceIndex);
                    }
                    if (choiceIndex == carIndex)
                        wins++;
                    else
                        loss++;
                }

                var scoreBoard = _scoreBoardFactory.Create(wins, loss, command.Tries, command.IsSwitchStrategyEnabled);
                return new SuccessCommandResult<PayloadResponse<ScoreBoard>>(
                    new PayloadResponse<ScoreBoard>
                    {
                        Payload = scoreBoard
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred whilst attempting to play");
                return new UnexpectedCommandResult<PayloadResponse<ScoreBoard>>();
            }
        }
    }
}
