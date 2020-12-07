using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    internal static class Program
    {
        private static Dictionary<string, BagContent[]> _dict;
        
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");

            _dict = new Dictionary<string, BagContent[]>();

            foreach (var line in lines)
            {
                var rule = line.Split("s contain ");

                var contents = rule[1]
                    .Split(", ")
                    .Where(x => x != "no other bags.")
                    .Select(x =>
                    {
                        var quantity = int.Parse(new string(x.Where(char.IsDigit).ToArray()));
                        var color = x[(x.IndexOf(' ') + 1)..].TrimEnd('.', 's');
                        return new BagContent(quantity, color);
                    }).ToArray();

                _dict.Add(rule[0], contents);
            }

            var atLeastOneShinyGoldBag = _dict.Sum(d => CanHold(d.Key) ? 1 : 0) - 1;

            Console.WriteLine(atLeastOneShinyGoldBag); // 211

            var requiredBags = RequiredBags("shiny gold bag") - 1;

            Console.WriteLine(requiredBags); //  12414
        }

        private static bool CanHold(string color)
        {
            return color == "shiny gold bag" ||
                   _dict[color].Aggregate(false, (current, d) => current || CanHold(d.Color));
        }

        private static int RequiredBags(string color)
        {
            return _dict[color].Aggregate(1, (current, d) => current + d.Quantity * RequiredBags(d.Color));
        }

        private record BagContent(int Quantity, string Color);
    }
}