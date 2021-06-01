using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpObserveVersions
{
    static class Program
    {
        static async Task Main(string[] args)
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
            _ = Task.Run(() => { });

            //1.2 Pattern matching
            //is
            int? input = null;
            if (input is int intInput)
                //can access intInput
                Console.WriteLine(intInput);
            else
                //cant
                //Console.WriteLine(intInput);
                Console.WriteLine("oops");

            var reference = new object();
            if (reference is not null && reference is IDisposable disposable)
                disposable.Dispose();

            var state = PerformOperation(Operation.Start);

            var waterState = WaterState(11);
            Console.WriteLine(waterState);

            //1.3 Local functions, two use cases with deferred execution 
            Enumerable.Range(0, 10).CustomWhere(i => i > 2);

            await PerformLongRunningWork("address", 1);
        }

        public static IEnumerable<T> CustomWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            return CustomerWhereImplementation();

            IEnumerable<T> CustomerWhereImplementation()
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                        yield return item;
                }
            }
        }

        public static Task<string> PerformLongRunningWork(string address, int index)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"{address} is required");
            if(index < 0)
                throw new ArgumentException($"{index} should be positive");

            return LongRunningWorkImplementation();

            async Task<string> LongRunningWorkImplementation()
            {
                var result1 = await Task.FromResult("123");
                var result2 = await Task.FromResult(1);

                return $"{result1}-{result2}";
            }
        }

        private static string WaterState(int tempInFahrenheit) =>
            tempInFahrenheit switch
            {
                (> 32) and (< 212) => "liquid",
                < 32 => "solid",
                > 212 => "gas",
                _ => "transition"
            };

        private enum Operation
        {
            Start,
            Stop
        }

        private enum State
        {
            Running,
            Stopped
        }
        private static State PerformOperation(Operation command) =>
        command switch
        {
            Operation.Start => State.Running,
            Operation.Stop => State.Stopped,
            _ => throw new ArgumentException(nameof(command), "Invalid enum value for command"),
        };

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
