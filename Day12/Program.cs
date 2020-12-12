using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            
            var actions = lines.Select(s => new Action(s[0], int.Parse(s[1..]))).ToArray();

            var dist1 = Part1(actions);

            Console.WriteLine(dist1); //  1565

            var dist2 = Part2(actions);

            Console.WriteLine(dist2); //  78883
        }

        private static int Part1(IEnumerable<Action> actions)
        {
            int x = 0, y = 0, d = 1;

            foreach (var (c, arg) in actions)
            {
                switch (c)
                {
                    case 'N':
                        y -= arg;
                        break;
                    case 'S':
                        y += arg;
                        break;
                    case 'E':
                        x += arg;
                        break;
                    case 'W':
                        x -= arg;
                        break;
                    case 'L':
                    {
                        d = (4 + d - arg / 90) % 4;
                        break;
                    }
                    case 'R':
                    { 
                        d = (4 + d + arg / 90) % 4;
                        break;
                    }
                    case 'F':
                        switch (d)
                        {
                            case 0:
                                y -= arg;
                                break;
                            case 1:
                                x += arg;
                                break;
                            case 2:
                                y += arg;
                                break;
                            case 3:
                                x -= arg;
                                break;
                        }

                        break;
                }
            }

            var dist = Math.Abs(x) + Math.Abs(y);

            return dist;
        }

        private static int Part2(IEnumerable<Action> actions)
        {
            int x = 0, y = 0, e = 10, n = -1;

            foreach (var (c, arg) in actions)
            {
                switch (c)
                {
                    case 'N':
                        n -= arg;
                        break;
                    case 'S':
                        n += arg;
                        break;
                    case 'E':
                        e += arg;
                        break;
                    case 'W':
                        e -= arg;
                        break;
                    case 'L':
                    {
                        for (var i = 0; i < arg / 90; i++)
                        {
                            var temp = n;
                            n = -e;
                            e = temp;
                        }

                        break;
                    }
                    case 'R':
                    {
                        for (var i = 0; i < arg / 90; i++)
                        {
                            var temp = e;
                            e = -n;
                            n = temp;
                        }

                        break;
                    }
                    case 'F':
                        x += e * arg;
                        y += n * arg;
                        break;
                }
            }

            var dist = Math.Abs(x) + Math.Abs(y);

            return dist;
        }

        private record Action(char A, int Arg);
    }
}