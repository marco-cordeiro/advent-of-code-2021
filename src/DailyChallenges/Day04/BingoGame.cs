using System;
using System.Collections.Generic;

namespace AdventOfCode2020.DailyChallenges.Day04
{
    public class BingoGame
    {
        public BingoGame()
        {
            Numbers = Array.Empty<int>();
            Boards = Array.Empty<BingoBoard>();
        }

        public IEnumerable<int> Numbers { get; init; }
        public BingoBoard[] Boards { get; init; }
    }
}