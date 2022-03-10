using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Day15
{
    public class MemoryItem
    {
        public long Number { get; set; }

        public List<long> Turns { get; set; } = new();
    }

    internal static class Program
    {
        private static void Main()
        {
            const string input = "0,3,6";

            var numbers = input.Split(',').Select(long.Parse).ToArray();
            
            var history = numbers.Select((x, n) => new MemoryItem
            {
                Number = x,
                Turns = new List<long> {n + 1}
            }).ToList();

            var i = 3;

            var last = numbers.Last();

            while (i < 2020)
            {
                var foo = history.SingleOrDefault(x => x.Number == last);

                if (foo == null)
                {
                    last = 0;
                    history[0].Turns.Add(i);
                }
                else
                {
                    var last2 = foo.Turns.TakeLast(2).ToArray();
                    last = last2[1] - last2[0];

                    var bar = history.SingleOrDefault(x => x.Number == last);
                    if (bar == null)
                    {
                        history.Add(new MemoryItem { Number = last, Turns = new List<long>{i}});
                    }
                    else
                    {
                        bar.Turns.Add(i);
                    }
                }

                i++;
            }


            Console.WriteLine(""); // 
        }
    }
}