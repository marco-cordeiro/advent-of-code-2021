using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day01
{
    public class ChallengeDay1 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<int> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay1(IDataProvider<int> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 1;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).ToArray();
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<int> data)
        {
            var answer = data.CountDepthIncreases();
            _output.WriteLine($"\tThe depth increased {answer} times");
        }

        private void ResolvePart2(IEnumerable<int> data)
        {
            var answer = data
                .AsThreeMeasurementWindows()
                .CountDepthIncreases();
            
            _output.WriteLine($"\tThe depth increased {answer} times");
        }
    }
}