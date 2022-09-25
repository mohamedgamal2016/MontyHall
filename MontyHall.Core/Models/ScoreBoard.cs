namespace MontyHall.Core.Models
{
    public class ScoreBoard
    {
        public string Strategy { get; set; }
        public int WinCount { get; set; }
        public float WinPercentage { get; set; }
        public int LossCount { get; set; }
        public float LossPercentage { get; set; }
    }
}
