using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt").ToList();

            //var part1 = Part1(lines);

            //Console.WriteLine(part1); // 4207
            
            var part2 = Part2(lines);

            Console.WriteLine(part2); // 725850285300475
        }

        private static int Part1(IReadOnlyList<string> lines)
        {
            var deps = new Dictionary<int, int>();

            var timestamp = int.Parse(lines[0]);

            var busIds = lines[1].Split(',').Where(x => x != "x").Select(int.Parse);

            foreach (var bus in busIds)
            {
                var i = 0;

                while (i < timestamp)
                {
                    i += bus;
                }

                deps.Add(bus, i);
            }

            var (busId, earliest) = deps.OrderBy(x => x.Value).First();

            var part1 = busId * (earliest - timestamp);

            return part1;
        }
        
        private static long Part2(IReadOnlyList<string> lines)
        {
            var busIds = lines[1].Split(',');

            var schedule = busIds
                .Select((x, i1) => (Id: x, Offset: i1))
                .Where(x => x.Id != "x")
                .Select(x => (Id: long.Parse(x.Id), Offset: (long)x.Offset))
                .ToArray();

            var increment = schedule[0].Id;

            var busIndex = 1;

            long i;

            for (i = schedule[0].Id; busIndex < schedule.Length; i += increment)
            {
                if ((i + schedule[busIndex].Offset) % schedule[busIndex].Id != 0)
                {
                    continue;
                }

                increment *= schedule[busIndex].Id;
                busIndex++;
            }

            return i - increment;
        }
    }
}