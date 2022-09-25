namespace MontyHall.Models.Request
{
    public class PlayGameRequest
    {
        public int Tries { get; set; }

        public bool IsSwitchStrategyEnabled { get; set; }
    }
}
