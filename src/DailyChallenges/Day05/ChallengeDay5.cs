using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day05
{
    public class ChallengeDay5 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay5(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 5;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var lines = _dataProvider.Read(Day).AsLines().ToArray();
                
            ResolvePart1(lines);
            ResolvePart2(lines);
        }

        private void ResolvePart1(IEnumerable<Line> lines)
        {
            var map = new byte[1000, 1000];
            map.DrawLines(lines.ExcludeDiagonals());

            var values = map.ToEnumerable().Count(x => x > 1);

            _output.WriteLine($"\tThere are {values} points overlapping vent lines (excluding diagonals)");
        }

        private void ResolvePart2(IEnumerable<Line> lines)
        {
            var map = new byte[1000, 1000];
            map.DrawLines(lines);

            var values = map.ToEnumerable().Count(x => x > 1);

            _output.WriteLine($"\tThere are {values} points overlapping vent lines");
        }
    }
}