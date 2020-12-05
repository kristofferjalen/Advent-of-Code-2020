using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            
            // A
            var treesA = Trees(3, 1, lines);

            Console.WriteLine(treesA); // 272

            // B
            var prod = new (int r, int b)[]
            {
                (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)
            }.Aggregate(1L, (acc, next) => acc * Trees(next.r, next.b, lines));
            
            Console.WriteLine(prod); // 3898725600
        }

        private static int Trees(int r, int b, IReadOnlyList<string> lines)
        {
            int x = 0, y = 0, trees = 0;

            while (y < lines.Count - 1)
            {
                x = (x + r) % lines[0].Length;
                y += b;
                trees += lines[y][x] == '#' ? 1 : 0;
            }

            return trees;
        }
    }
}