using System;
using System.IO;
using System.Linq;

namespace Day06
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllText("input.txt");

            var groups = lines.Split("\n\n");

            int sumA = 0, sumB = 0;

            foreach (var group in groups)
            {
                var persons = group.Split("\n");

                var questions = persons.SelectMany(x => x.ToCharArray()).Distinct().ToArray();

                sumA += questions.Length;

                sumB += questions.Count(x => persons.All(y => y.Contains(x)));
            }

            Console.WriteLine(sumA); // 6437
            Console.WriteLine(sumB); // 3229
        }
    }
}