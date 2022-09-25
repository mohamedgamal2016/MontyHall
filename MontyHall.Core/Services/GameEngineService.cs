using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Extensions;
using MontyHall.Core.Common.Response;
using MontyHall.Core.Factories;
using MontyHall.Core.Models;

namespace MontyHall.Core.Services
{
    public class GameEngineService : IGameEngineService
    {
        private readonly IDoorFactory _doorFactory;

        private int[] DOOR_INDEXES = new[] { 0, 1, 2 };
        private List<Door> Doors { get; set; }


        public GameEngineService(IDoorFactory doorFactory)
        {
            _doorFactory = doorFactory;
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

                var scoreBoard = new ScoreBoard
                {
                    Strategy = command.IsSwitchStrategyEnabled ? "Switch Strategy" : "Stay Strategy",
                    WinCount = wins,
                    WinPercentage = ((float)wins / command.Tries * 100),
                    LossCount = loss,
                    LossPercentage = ((float)loss / command.Tries * 100),
                };


                var result = new SuccessCommandResult<PayloadResponse<ScoreBoard>>(
                    new PayloadResponse<ScoreBoard>
                    {
                        Payload = scoreBoard
                    });
                return result;
            }
            catch (Exception ex)
            {
                return new UnexpectedCommandResult<PayloadResponse<ScoreBoard>>();
            }
        }
    }
}
