using MontyHall.Core.Models;

namespace MontyHall.Core.Factories
{
    public class ScoreBoardFactory : IScoreBoardFactory
    {
        public ScoreBoard Create(int win, int loss, int tries, bool isSwitchStrategyEnabled)
        {
            return new ScoreBoard {
                Strategy = isSwitchStrategyEnabled ? "Switch Strategy" : "Stay Strategy",
                WinCount = win,
                WinPercentage = ((float)win / tries * 100),
                LossCount = loss,
                LossPercentage = ((float)loss / tries * 100),
            };
        }
    }
}
