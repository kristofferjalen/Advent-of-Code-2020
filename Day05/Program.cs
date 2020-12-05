using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var highest = 0d;
            var seats = new List<int>();

            foreach (var line in lines)
            {
                int minRow = 0, maxRow = 127, minCol = 0, maxCol = 7;

                foreach (var c in line)
                {
                    maxRow = c == 'F' ? (int) Math.Floor(minRow + (maxRow - minRow) / 2d) : maxRow;
                    minRow = c == 'B' ? (int) Math.Ceiling(minRow + (maxRow - minRow) / 2d) : minRow;
                    maxCol = c == 'L' ? (int) Math.Floor(minCol + (maxCol - minCol) / 2d) : maxCol;
                    minCol = c == 'R' ? (int) Math.Ceiling(minCol + (maxCol - minCol) / 2d) : minCol;
                }

                var seatId = minRow * 8 + minCol;

                highest = Math.Max(highest, seatId);

                seats.Add(seatId);
            }

            var ordered = seats.OrderBy(x => x).ToArray();
            var i = 0;
            while (ordered[++i + 1] == ordered[i] + 1) { }

            Console.WriteLine(highest); // 963
            Console.WriteLine(ordered[i] + 1); // 592
        }
    }
}