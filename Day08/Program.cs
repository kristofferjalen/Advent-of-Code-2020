using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day08;

var programCode = File.ReadAllLines("input.txt");
var memoryItems = programCode
    .Select(x => new MemoryItem(x.Split(' ')[0], int.Parse(x.Split(' ')[1])))
    .ToArray();

// Part 1
var part1 = Part1(memoryItems);
Console.WriteLine(part1); // 1744

// Part 2
var part2 = Part2(memoryItems);
Console.WriteLine(part2); // 1174

static int Part1(MemoryItem[] memoryItems)
{
    var cmp = new Computer
    {
        Memory = memoryItems
    };

    var visited = new HashSet<int>();

    while (visited.Add(cmp.Pc))
        Computer.OpCodeActions[cmp.Memory[cmp.Pc].OpCode](cmp, cmp.Memory[cmp.Pc].Argument);

    return cmp.A;
}

static int Part2(MemoryItem[] memoryItems)
{
    var cmp = new Computer
    {
        Memory = memoryItems
    };

    var triedFlipped = new HashSet<int>();
    var eof = false;

    while (!eof)
    {
        cmp.A = 0;
        cmp.Pc = 0;
        var visited = new HashSet<int>();
        var currentFlipped = -1;

        while (true)
        {
            // Reached end of file, finishing
            if (cmp.Pc == cmp.Memory.Length)
            {
                eof = true;
                break;
            }

            // Check if already visited
            var alreadyVisited = !visited.Add(cmp.Pc);
            if (alreadyVisited)
            {
                // Revert back flip
                cmp.Memory[currentFlipped] = cmp.Memory[currentFlipped].OpCode switch
                {
                    "jmp" => new MemoryItem("nop", cmp.Memory[currentFlipped].Argument),
                    "nop" => new MemoryItem("jmp", cmp.Memory[currentFlipped].Argument),
                    _ => cmp.Memory[currentFlipped]
                };
                break; // infinite loop
            }

            // Current opcode
            var (opcode, _) = cmp.Memory[cmp.Pc];

            // If have not flipped in this run yet, and JMP or NOP, and location has not been flipped before, then try flip
            if (currentFlipped == -1 && opcode != "acc" && !triedFlipped.Contains(cmp.Pc))
            {
                triedFlipped.Add(cmp.Pc);
                currentFlipped = cmp.Pc;
                cmp.Memory[cmp.Pc] = opcode switch
                {
                    "jmp" => new MemoryItem("nop", cmp.Memory[cmp.Pc].Argument),
                    "nop" => new MemoryItem("jmp", cmp.Memory[cmp.Pc].Argument),
                    _ => cmp.Memory[cmp.Pc]
                };
            }

            // Execute current
            Computer.OpCodeActions[cmp.Memory[cmp.Pc].OpCode](cmp, cmp.Memory[cmp.Pc].Argument);
        }
    }

    return cmp.A;
}
