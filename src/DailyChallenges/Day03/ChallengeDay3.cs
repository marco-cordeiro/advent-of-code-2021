using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day03
{
    public class ChallengeDay3 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay3(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 3;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).AsBinary().ToArray();

            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<BitArray> data)
        {
            var (gamma, epsilon) = data.GetRates();
            _output.WriteLine($"\tThe gamma rate is {gamma}");
            _output.WriteLine($"\tThe epsilon rate is {epsilon}");
            _output.WriteLine($"\tThe power consumption of the submarine is {gamma * epsilon}");
        }

        private void ResolvePart2(BitArray[] data)
        {
            var oxygenGeneratorRating = data.GetOxygenGeneratorRating();
            var co2ScrubberRating = data.GetCO2ScrubberRating();
            _output.WriteLine($"\tThe Oxygen Generator Rating is {oxygenGeneratorRating}");
            _output.WriteLine($"\tThe CO2 Scrubber Rating is {co2ScrubberRating}");
            _output.WriteLine($"\tThe Life Support Rating is {oxygenGeneratorRating * co2ScrubberRating}");
        }
    }
}