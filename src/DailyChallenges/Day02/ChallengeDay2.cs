using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day02
{
    public class ChallengeDay2 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay2(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 2;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).Select(x => x.ConvertToMovement()).ToArray();
            
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<Movement> movements)
        {
            var (depth, hPosition) = movements.Move(false);

            _output.WriteLine("\tSubmarine reached :");
            _output.WriteLine($"\t\tDepth : {depth}");
            _output.WriteLine($"\t\tHorizontal position : {hPosition}");
            _output.WriteLine($"\t\tFactor : {hPosition * depth}");
        }

        private void ResolvePart2(IEnumerable<Movement> movements)
        {
            var (depth, hPosition) = movements.Move(true);

            _output.WriteLine("\tSubmarine reached :");
            _output.WriteLine($"\t\tDepth : {depth}");
            _output.WriteLine($"\t\tHorizontal position : {hPosition}");
            _output.WriteLine($"\t\tFactor : {hPosition * depth}");
        }
    }
}