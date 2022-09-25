using System;
using System.Collections.Generic;
using System.Text;

namespace MontyHall.Core.Common.Extensions
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, List<T> list)
        {
            int length = list.Count;
            while (length > 1)
            {
                int index = rng.Next(length--);
                T temp = list[length];
                list[length] = list[index];
                list[index] = temp;
            }
        }
    }
}
