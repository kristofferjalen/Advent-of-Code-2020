using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Day11
{
    internal static class Program
    {
        private static void Main()
        {
            var lines = File.ReadAllLines("input.txt");

            var grid = lines.Select(x => x.ToCharArray()).ToArray();

            var width = lines[0].Length;
            var heigth = lines.Length;

            var seats1 = Part1(grid, heigth, width);

            Console.WriteLine(seats1); // 2211


            var seats2 = Part2(grid, heigth, width);

            Console.WriteLine(seats2); // 1995

        }

        private static int Part1(char[][] grid, int heigth, int width)
        {
            var count = 0;

            while (count++ < 220)
            {
                var next = grid.Select(x => (char[]) x.Clone()).ToArray();

                for (var i = 0; i < heigth; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        if (grid[i][j] == 'L')
                        {
                            var occ = 0;
                            occ += i >= 1 && j >= 1 && grid[i - 1][j - 1] == '#' ? 1 : 0;
                            occ += i >= 1 && grid[i - 1][j] == '#' ? 1 : 0;
                            occ += i >= 1 && j < width - 1 && grid[i - 1][j + 1] == '#' ? 1 : 0;
                            occ += j >= 1 && grid[i][j - 1] == '#' ? 1 : 0;
                            occ += j < width - 1 && grid[i][j + 1] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && j >= 1 && grid[i + 1][j - 1] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && grid[i + 1][j] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && j < width - 1 && grid[i + 1][j + 1] == '#' ? 1 : 0;

                            if (occ == 0)
                                next[i][j] = '#';
                        }

                        if (grid[i][j] == '#')
                        {
                            var occ = 0;
                            occ += i >= 1 && j >= 1 && grid[i - 1][j - 1] == '#' ? 1 : 0;
                            occ += i >= 1 && grid[i - 1][j] == '#' ? 1 : 0;
                            occ += i >= 1 && j < width - 1 && grid[i - 1][j + 1] == '#' ? 1 : 0;
                            occ += j >= 1 && grid[i][j - 1] == '#' ? 1 : 0;
                            occ += j < width - 1 && grid[i][j + 1] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && j >= 1 && grid[i + 1][j - 1] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && grid[i + 1][j] == '#' ? 1 : 0;
                            occ += i < heigth - 1 && j < width - 1 && grid[i + 1][j + 1] == '#' ? 1 : 0;

                            if (occ >= 4)
                                next[i][j] = 'L';
                        }
                    }
                }

                grid = next;
            }

            var seats = grid.Sum(x => x.Count(y => y == '#'));
            return seats;
        }

        private static int Part2(char[][] grid, int heigth, int width)
        {
            var count = 0;

            while (count++ < 1000)
            {
                var next = grid.Select(x => (char[])x.Clone()).ToArray();

                for (var i = 0; i < heigth; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        if (grid[i][j] == 'L')
                        {
                            var occ = 0;
                            int x = 0, y = 0;


                            x = j - 1; 
                            y = i - 1;
                            while (x >= 0 && y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }
                                
                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                                y--;
                            }
                            x = j;
                            y = i - 1;
                            while (y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                y--;
                            }
                            x = j + 1;
                            y = i - 1;
                            while (x < width && y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                                y--;
                            }
                            x = j - 1;
                            y = i;
                            while (x >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                            }
                            x = j + 1;
                            y = i;
                            while (x < width)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                            }
                            x = j - 1;
                            y = i + 1;
                            while (x >= 0 && y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                                y++;
                            }
                            x = j;
                            y = i + 1;
                            while (y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                y++;
                            }
                            x = j + 1;
                            y = i + 1;
                            while (x < width && y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                                y++;
                            }
                            
                            if (occ == 0)
                                next[i][j] = '#';
                        }

                        if (grid[i][j] == '#')
                        {
                            var occ = 0;
                            int x = 0, y = 0;


                            x = j - 1;
                            y = i - 1;
                            while (x >= 0 && y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                                y--;
                            }
                            x = j;
                            y = i - 1;
                            while (y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                y--;
                            }
                            x = j + 1;
                            y = i - 1;
                            while (x < width && y >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                                y--;
                            }
                            x = j - 1;
                            y = i;
                            while (x >= 0)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                            }
                            x = j + 1;
                            y = i;
                            while (x < width)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                            }
                            x = j - 1;
                            y = i + 1;
                            while (x >= 0 && y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x--;
                                y++;
                            }
                            x = j;
                            y = i + 1;
                            while (y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                y++;
                            }
                            x = j + 1;
                            y = i + 1;
                            while (x < width && y < heigth)
                            {
                                if (grid[y][x] == 'L')
                                {
                                    break;
                                }

                                if (grid[y][x] == '#')
                                {
                                    occ++;
                                    break;
                                }
                                x++;
                                y++;
                            }

                            if (occ >= 5)
                                next[i][j] = 'L';
                        }
                    }
                }

                grid = next;
            }

            var seats = grid.Sum(x => x.Count(y => y == '#'));
            return seats;
        }
    }
}