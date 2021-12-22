using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day09
{
    public class ChallengeDay9 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay9(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 9;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var map = _dataProvider.Read(Day).ReadCaveFloorHeightMap();

            ResolvePart1(map);
            ResolvePart2(map);
        }

        private void ResolvePart1(byte[,] map)
        {
            var lowPoints = map.FindLowPoints().ToArray();
            var risk = lowPoints.Sum(x => map[x.y, x.x] + 1);
            _output.WriteLine($"\tThe sum of the risk levels of all low points is {risk}");
        }

        private void ResolvePart2(byte[,] map)
        {   
            var lowPoints = map.FindLowPoints().ToArray();
            var basinSizes = lowPoints.Select(x => map.FindBasinSize(x.y, x.x));
            var largestBasins = basinSizes
                .OrderByDescending(x => x)
                .Take(3).ToArray();
                var result = largestBasins
                .Aggregate((t, s) => t * s);
            _output.WriteLine($"\tThe product of the 3 biggest basins is {result}");
        }
    }
}