using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            
            var numbers = lines.Select(long.Parse).ToArray();
            
            var part1 = Part1(numbers);

            Console.WriteLine(part1); // 22406676

            var part2 = Part2(numbers, part1);

            Console.WriteLine(part2); // 2942387
        }

        private static long Part1(IReadOnlyList<long> numbers)
        {
            const int preamble = 25;

            var rest = numbers.Skip(preamble).ToArray();

            var i = -1;

            var isSumOfTwo = true;

            while (i < rest.Length && isSumOfTwo)
            {
                i++;

                var j = i;

                isSumOfTwo = false;

                while (j < i + preamble && !isSumOfTwo)
                {
                    var k = j + 1;

                    while (k < i + preamble && !isSumOfTwo)
                    {
                        isSumOfTwo = numbers[j] + numbers[k] == rest[i];

                        k++;
                    }

                    j++;
                }
            }

            return rest[i];
        }


        private static long Part2(IReadOnlyList<long> numbers, long invalid)
        {
            int i = 0, j = 0;

            var sum = 0L;

            while (i < numbers.Count && sum != invalid)
            {
                sum = numbers[i];

                j = i + 1;

                while (j < numbers.Count && sum != invalid)
                {
                    sum += numbers[j];

                    j++;
                }

                i++;
            }

            var range = numbers
                .Skip(i - 1)
                .Take(j - 1 - (i - 1) + 1)
                .OrderBy(x => x)
                .ToArray();

            var weakness = range[0] + range.Last();
            
            return weakness;
        }
    }
}