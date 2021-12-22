using System;

namespace AdventOfCode2020.DailyChallenges.Day08
{
    public class DisplayData
    {
        public DisplayData()
        {
            Signal = Array.Empty<string>();
            Output = Array.Empty<string>();
        }

        public string[] Signal { get; init; }
        public string[] Output { get; init; }
    }
}