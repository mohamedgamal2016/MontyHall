using MontyHall.Core.Models;

namespace MontyHall.Core.Factories
{
    public interface IScoreBoardFactory
    {
        ScoreBoard Create(int win, int loss, int tries, bool isSwitchStrategyEnabled);
    }
}
