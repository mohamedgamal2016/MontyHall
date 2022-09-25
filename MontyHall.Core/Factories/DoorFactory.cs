using System.Collections.Generic;
using MontyHall.Core.Models;

namespace MontyHall.Core.Factories
{
    public class DoorFactory : IDoorFactory
    {
        public IEnumerable<Door> Create()
        {
            return new List<Door>
            {
                new Door {
                    IsPrize = false,
                    Name = "Goat"
                },
                new Door
                {
                    IsPrize = false,
                    Name = "Goat"
                },
                new Door
                {
                    IsPrize = true,
                    Name = "Car"
                }
            };
        }
    }
}
