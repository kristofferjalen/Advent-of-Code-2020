using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    public class Segment {

        public char[] Mask { get; init; }
        public List<(long Location, long Value)> Instructions { get; init; }
    }

    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");

            var segments = new List<Segment>();

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("mask"))
                {
                    var mask = lines[i].Split("mask = ")[1].ToCharArray();
                    segments.Add(new Segment {Mask = mask, Instructions = new List<(long Location, long Value)>()});
                    continue;
                }

                var foo = lines[i].Split(" = ");
                var bar = (Location: long.Parse(string.Join("", foo[0].Where(char.IsDigit))), Value: long.Parse(foo[1]));
                segments.Last().Instructions.Add(bar);
            }
            
            var mem = segments.SelectMany(x => x.Instructions.Select(y => y.Location)).Distinct().ToDictionary(x => x, x => 0L);

            //var sum = Part1(segments, mem);

            //Console.WriteLine(sum); // 11327140210986


        }

        private static long Part2(List<Segment> segments, Dictionary<long, long> mem)
        {
            
        }

        private static long Part1(List<Segment> segments, Dictionary<long, long> mem)
        {
            foreach (var segment in segments)
            {
                var instructions = segment.Instructions;
                var mask = segment.Mask;

                for (var j = 0; j < instructions.Count; j++)
                {
                    var (location, toWrite) = instructions[j];
                    //var bytes = BitConverter.GetBytes(m.Value).ToArray();
                    //var value = new BitArray(bytes);

                    var toWriteArray = Convert.ToString(toWrite, 2).PadLeft(36, '0').ToCharArray();

                    // Apply mask
                    var i = mask.Length - 1;
                    while (i >= 0)
                    {
                        if (mask[i] == '1')
                        {
                            toWriteArray[i] = '1';
                        }

                        if (mask[i] == '0')
                        {
                            toWriteArray[i] = '0';
                        }

                        i--;
                    }

                    // Write value
                    var s = new string(toWriteArray);
                    var write = Convert.ToInt64(s, 2);

                    mem[location] = write;
                }
            }

            var sum = mem.Sum(x => x.Value);
            return sum;
        }
    }
}