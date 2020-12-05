using System;
using System.IO;
using System.Linq;

namespace Day02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            int total1 = 0, total2 = 0;

            foreach (var line in lines)
            {
                var p = line.Split(new[] {' ', ':', '-'}, StringSplitOptions.RemoveEmptyEntries);

                var first = int.Parse(p[0]);
                var second = int.Parse(p[1]);
                var c = p[2][0];
                var pwd = p[3];

                var count = pwd.Count(x => x == c);
                var ok1 = count >= first && count <= second;
                
                var ok2 = pwd[first - 1] == c ^ pwd[second - 1] == c;

                total1 += ok1 ? 1 : 0;
                total2 += ok2 ? 1 : 0;
            }

            Console.WriteLine(total1); // 477
            Console.WriteLine(total2); // 686
        }
    }
}