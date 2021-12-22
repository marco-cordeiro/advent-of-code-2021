using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day08
{
    public class ChallengeDay8 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay8(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 8;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).ReadSubmarineDisplayData().ToArray();
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<DisplayData> data)
        {
            var digits = data.SelectMany(x => x.Output).Count(x => x.Length is 2 or 3 or 4 or 7);
            _output.WriteLine($"\tThe output value has {digits} unique occurrences");
        }

        private void ResolvePart2(IEnumerable<DisplayData> data)
        {
            var result = data.Select(x=>x.Analyze().Decode(x.Output)).Sum();
            _output.WriteLine($"\tThe sum of all output values is {result}");
        }
    }
}