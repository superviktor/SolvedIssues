using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpObserveVersions
{
    class Program
    {
        static void Main(string[] args)
        {
            //1 C# 7.0 - 7.3
            //1.1 Tuples and discards
            (string Alpha, string Beta) namedLetters = ("a", "b");
            Console.WriteLine($"{namedLetters.Alpha}, {namedLetters.Beta}");
            var alphabetStart = (Alpha: "a", Beta: "b");
            Console.WriteLine($"{alphabetStart.Alpha}, {alphabetStart.Beta}");
            //deconstruct tuple
            (int min, int max) = MinMax(new[] { 10, -2, 7, -8 });
            Console.WriteLine(min);
            Console.WriteLine(max);
            //type deconstruction 
            var point = new Point(1, 2);
            (int x, int y) = point;
            Console.WriteLine($"{x},{y}");
            //discard 
            (_, _, string lastName) = ("Armin", "van", "Buuren");
            Console.WriteLine(lastName);
            //standalone discard
            LookInside(new object());
            _ = Task.Run(() =>{});

            //1.2 Pattern matching
        }

        private static void LookInside(object arg)
        {
            _ = arg ?? throw new ArgumentNullException(nameof(arg));
        }

        private static (int min, int max) MinMax(int[] numbers)
        {
            var list = numbers.ToList().OrderBy(x => x);
            return (list.First(), list.Last());
        }

        private class Point
        {
            public Point(int x, int y)
            {
                (X, Y) = (x, y);
            }
            public int X { get; }
            public int Y { get; }

            public void Deconstruct(out int x, out int y)
            {
                (x, y) = (X, Y);
            }
        }
    }
}
