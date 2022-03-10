using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");

            var numbers = lines.Select(int.Parse).ToList();

            var prod = Part1(numbers);

            Console.WriteLine(prod == 2263); // 2263
            
            var part2 = Part2(numbers);

            Console.WriteLine(part2 == 396857386627072); // 396857386627072
        }
        
        private static int Part1(IReadOnlyCollection<int> numbers)
        {
            int diff1 = 0, diff3 = 0, last = 0, j = 0;

            while (j++ < numbers.Count)
            {
                var min = numbers
                    .Where(x => new[] {last + 1, last + 2, last + 3}.Contains(x))
                    .Min();

                diff3 += min - last == 3 ? 1 : 0;
                
                diff1 += min - last == 1 ? 1 : 0;
                
                last = min;
            }

            var prod = diff1 * (diff3 + 1);

            return prod;
        }
        
        private static long Part2(ICollection<int> numbers)
        {
            numbers.Add(0);
            numbers.Add(numbers.Max() + 3);

            var adaptors = numbers.OrderBy(x => x).ToArray();

            var counts = Enumerable.Repeat(0L, adaptors.Length).ToArray();

            counts[^1] = 1;

            var reversed = Enumerable.Repeat(0, adaptors.Length - 1).Reverse().ToArray();

            var i = reversed.Length - 1;

            while (i >= 0)
            {
                var s = 0L;

                var foo = adaptors[(i + 1)..];

                var j = 0;

                while (j < foo.Length && foo[j] - adaptors[i] <= 3)
                {
                    s += counts[i + j + 1];
                    j++;
                }

                counts[i] = s;

                i--;
            }

            return counts[0];
        }
    }
}