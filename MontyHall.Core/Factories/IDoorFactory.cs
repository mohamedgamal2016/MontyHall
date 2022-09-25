using MontyHall.Core.Models;
using System.Collections.Generic;

namespace MontyHall.Core.Factories
{
    public interface IDoorFactory
    {
        IEnumerable<Door> Create();
    }
}
