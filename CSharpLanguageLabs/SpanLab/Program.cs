using System;
using System.Diagnostics;

namespace SpanLab
{
    class Program
    {
        private static int Count = 1000000000;

        static void Main(string[] args)
        {
            //StringSplit();
            SpanSplit();
            Console.ReadLine();

        }

        public static void StringSplit()
        {
            var word = "10+20";

            var splitIndex = word.IndexOf("+");

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Count; i++)
            {
                var num1 = int.Parse(word.Substring(0, splitIndex));

                var num2 = int.Parse(word.Substring(splitIndex + 1));

                var sum = num1 + num2;
            }

            sw.Stop();

            Console.WriteLine($"StringSplit {sw.ElapsedMilliseconds}ms");
        }

        public static void SpanSplit()
        {
            var word = "10+20";

            var splitIndex = word.IndexOf("+");

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Count; i++)
            {
                var num1 = int.Parse(word.AsSpan(0, splitIndex));

                var num2 = int.Parse(word.AsSpan(splitIndex + 1));

                var sum = num1 + num2;
            }

            sw.Stop();

            Console.WriteLine($"SpanSplit {sw.ElapsedMilliseconds}ms");
        }
    }
}
