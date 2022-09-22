using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a function that takes an array of items, removes all duplicate items and returns a new array in the same sequential order as the old array (minus duplicates).
            // Examples
            // removeDups([1, 0, 1, 0]) ➞ [1, 0]
            // removedups(["the", "big", "cat"]) ➞ ["the", "big", "cat"]
            // removedups(["john", "taylor", "john"]) ➞ ["john", "taylor"]

            void removeDups(object[] arr) {
                HashSet<object> lookup = new HashSet<object>();

                for (int i = 0; i < arr.Length; i++)
                {
                    lookup.Add(arr[i]);
                }

                foreach (var item in lookup)
                {
                    Console.WriteLine(item + " ");
                }
            }
            object[] a = new string[] { "john", "taylor", "john" };

            removeDups(a);

            Console.ReadLine();
        }
           
    }

    //void eatAll(List<Animal> animals)
    //{
    //    foreach (var animal in animals)
    //    {
    //        animal.eat();
    //    }
    //}

    //void eat(Animal animal)
    //{
    //    animal.eat();
    //}

    //var list = new List<Animal>() {
    //    new Animal(),
    //    new Dog(),
    //    new Cat(),
    //    new Mouse()
    //};

    ////eatAll(list);
    //eat(new Cat());
    //eat(new Dog());
    //eat(new Mouse());

    class Animal
    {
        public string food { get; set; } = "Base food";
        public virtual void eat()
        {
            Console.WriteLine("eating " + food);
        }
    }

    class Cat : Animal
    {
        public new string food { get; set; } = "Cat food";
        public override void eat()
        {
            Console.WriteLine("eating " + food);
        }
    }

    class Dog : Animal
    {
        public string food { get; set; } = "Dog food";

        public override void eat()
        {
            Console.WriteLine("eating " + food);

        }
    }

    class Mouse : Animal
    {
        public string food { get; set; } = "Mouse food";

        public override void eat()
        {
            Console.WriteLine("eating " + food);

        }
    }
}

// values = [2,3,5,9,12,13,17,29,42];
// bad_values = [3,5,6,11,13,56];

// result = [2, 9, 12, 17, 29, 42];

// values & bad_values could be very large in size, 100s or 1000s
// both values & bad_values are sorted


//        Console.WriteLine(IsValid("((()))"));
//        Console.WriteLine(IsValid("()()()"));
//        Console.WriteLine(IsValid("(()))("));
//        Console.WriteLine(IsValid("(())))"));
//        Console.WriteLine(IsValid(")("));

//        Console.Read();


//    public static int[] TwoNumberSum(int[] array, int targetSum)
//    {
//        // Write your code here.
//        if (array.Length > 1)
//        {
//            var res = new int[2];
//            var lookup = new Dictionary<int, int>();
//            for (int i = 0; i < array.Length; i++)
//            {
//                int target = targetSum - array[i];
//                if (!lookup.ContainsKey(target))
//                {
//                    lookup.Add(array[i], target);
//                }
//                else
//                {
//                    res[0] = lookup[target];
//                    res[1] = target;
//                }
//            }

//            return res;
//        }
//        return new int[0];
//    }

//    static bool IsValid(string s) {
//        Stack<char> lookup = new Stack<char>();
//        for (int i = 0; i < s.Length; i++)
//        {
//            if (s[i] == '(')
//            {
//                lookup.Push(s[i]);
//            }
//            else
//            {
//                if (lookup.Count == 0) return false;
//                else
//                {
//                    lookup.Pop();
//                }
//            }
//        }

//        return lookup.Count == 0;

//    }
//}

//#include <iostream>
//using namespace std;

//int main()
//{
//int n, m;
//cin >> n >> m;
//bool appendLast = true, sameRow = false;

//for (int i = 0; i < n; i++)
//{
//    for (int j = 0; j < m; j++)
//    {
//        if (i % 2 == 0)
//            cout << "#";

//        else if (i % 2 != 0 && j == m - 1 && appendLast && !sameRow)
//        {
//            cout << "#";
//            appendLast = false;
//        }
//        else if (i % 2 != 0 && j == 0 && !appendLast)
//        {
//            cout << "#";
//            appendLast = true;
//            sameRow = true;
//        }

//        else
//            cout << ".";
//    }
//    sameRow = false;
//    cout << endl;
//}


//    return 0;
//}
