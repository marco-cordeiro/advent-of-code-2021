using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day11
{
    public class ChallengeDay11 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay11(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 11;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).ToArray();

            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<string> data)
        {
            var map = data.ReadOctopusMap();
            var result = map.ExecuteSteps(100);
            _output.WriteLine($"\tAfter 100 steps the octopuses flashed {result} times");
        }

        private void ResolvePart2(IEnumerable<string> data)
        {
            var map = data.ReadOctopusMap();
            var result = map.ExecuteStepsUntilAllOctopusesFlash();
            _output.WriteLine($"\tAll octopuses flashed at step {result}");
        }
    }
}