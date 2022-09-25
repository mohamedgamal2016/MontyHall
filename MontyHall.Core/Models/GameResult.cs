using System;
using System.Collections.Generic;
using System.Text;

namespace MontyHall.Core.Models
{
    public class GameResult
    {
        public int Tries { get; set; }
        public int WinCount { get; set; }
        public int LossCount { get; set; }
    }
}
