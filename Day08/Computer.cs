using System;
using System.Collections.Generic;

namespace Day08
{
    internal class Computer
    {
        public static readonly Dictionary<string, Action<Computer, int>> OpCodeActions = new()
        {
            {
                "acc", (c, arg) =>
                {
                    c.A += arg;
                    c.Pc++;
                }
            },
            {"jmp", (c, arg) => c.Pc += arg},
            {"nop", (c, _) => c.Pc++}
        };

        public int A { get; set; }

        public MemoryItem[] Memory { get; init; } = Array.Empty<MemoryItem>();

        public int Pc { get; set; }
    }
}