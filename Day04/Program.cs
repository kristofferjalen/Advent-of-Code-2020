using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var lines = File.ReadAllText("input.txt");

            var passports = lines.Split("\n\n");

            // Part A
            var mandatory = new HashSet<string> {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

            var validsA = new List<string>();

            foreach (var passport in passports)
            {
                var fields = string.Join(" ", passport.Split("\n"))
                    .Split(' ')
                    .Select(x => x.Split(':')[0]);

                if (!mandatory.All(x => fields.Contains(x)))
                {
                    continue;
                }
                
                validsA.Add(passport);
            }

            Console.WriteLine(validsA.Count); // 254
            
            // Part B

            var actions = new Dictionary<string, Func<string, bool>>
            {
                {"byr", x => x.All(char.IsDigit) && int.Parse(x) >= 1920 && int.Parse(x) <= 2002},
                {"iyr", x => x.All(char.IsDigit) && int.Parse(x) >= 2010 && int.Parse(x) <= 2020},
                {"eyr", x => x.All(char.IsDigit) && int.Parse(x) >= 2020 && int.Parse(x) <= 2030},
                {"hgt", x =>
                    {
                        var number = int.Parse(string.Join("", x.Where(char.IsDigit)));
                        var unit = string.Join("", x.Where(y => !char.IsDigit(y)));
                        var valid = unit switch
                        {
                            "cm" => number >= 150 && number <= 193,
                            "in" => number >= 59 && number <= 76,
                            _ => false
                        };
                        return valid;
                    }
                },
                {"hcl", x =>
                    {
                        if (x.Length != 7)
                        {
                            return false;
                        }

                        var color = x.Substring(1);

                        var valid = color.All(c => int.TryParse(c.ToString(), out var i) && i >= 0 && i <= 9 || c >= 'a' && c <= 'f');

                        return valid;
                    }
                },
                {"ecl", x => new []{"amb","blu","brn","gry","grn","hzl","oth"}.Any(y => y == x)},
                {"pid", x => x.All(char.IsDigit) && x.Length == 9},
                {"cid", x => true},
            };

            var validsB = validsA
                .Select(passport => string.Join(" ", passport.Split("\n")).Split(' ').Select(x => x.Split(':')))
                .Select(fields => fields.Aggregate(true, (current, field) => current & actions[field[0]](field[1])))
                .Select(valid => valid ? 1 : 0).Sum();

            Console.WriteLine(validsB); //  184
        }
    }
}