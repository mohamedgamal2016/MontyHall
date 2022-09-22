using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Apple> apples = new List<Apple>();

            var list = new List<int> { 3, 2, 1 ,3 };
            int result = birthdayCakeCandles(list);
            Console.WriteLine(result);
            //miniMaxSum(list);


            Console.ReadLine();
        }

        public static int birthdayCakeCandles(List<int> candles)
        {
            var lookup = new Dictionary<int, int>();
            foreach (var candle in candles)
            {
                if (!lookup.ContainsKey(candle))
                    lookup.Add(candle, 1);

                else
                    lookup[candle]+= 1;

            }
            var maxKey = lookup.Keys.Max();
            return lookup[maxKey];
        }

        public static void miniMaxSum(List<int> arr)
        {
            long sum = 0;
            foreach (var item in arr)
            {
                sum += item;
            }
            int maxElement = arr.Max();
            int minElement = arr.Min();

            long minSum = sum - maxElement;
            long maxSum = sum - minElement;

            Console.WriteLine(minSum + " " + maxSum);
        }

        public class Factory
        {
            public void FillBoxes(List<Fruit> fruits)
            {
                if (fruits != null)
                {
                    Dictionary<Type, Box> boxes = new Dictionary<Type, Box>();
                
                    foreach (var fruit in fruits)
                    {
                        var type = fruit.GetType();
                        if (!boxes.ContainsKey(type))
                        {
                            boxes.Add(type, new Box());
                        }

                        var currentBox = boxes.GetValueOrDefault(type);

                        currentBox.Add(fruit);

                        if (!currentBox.CanAdd())
                        {
                            Console.WriteLine("The Box is full.");
                            boxes.Remove(type);

                        }
                    }
                }
            }
        }

        public class Fruit
        {
            public int Id { get; set; }

        }
        public class Apple: Fruit
        {
        }
        public class Orange : Fruit
        {
        }
        public class Kiwi : Fruit
        {
        }


        public class Box
        {
            public List<Fruit> Fruits { get; set; }
            public int Capacity { get; set; } 

            public Box()
            {
                Fruits = new List<Fruit>();
                Capacity = 10;
            }
            

            public bool CanAdd()
            {
                return Fruits.Count <= Capacity;
            }

            public void Add(Fruit fruit)
            {
                if (CanAdd())
                {
                    Fruits.Add(fruit);
                }
            }


        }

       
    }
}
