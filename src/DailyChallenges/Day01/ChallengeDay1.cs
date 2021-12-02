using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            ResolvePart1();
            ResolvePart2();
        }

        private void ResolvePart1()
        {
            var answer = _dataProvider.Read(Day).CountDepthIncreases();
            _output.WriteLine($"\tThe depth increased {answer} times");
        }

        private void ResolvePart2()
        {
            var answer = _dataProvider.Read(Day)
                .AsThreeMeasurementWindows()
                .CountDepthIncreases();
            
            _output.WriteLine($"\tThe depth increased {answer} times");
        }
    }

    public static class ChallengeSay1Extensions
    {
        public static IEnumerable<int> AsThreeMeasurementWindows(this IEnumerable<int> data)
        {
            var container = new Queue<int>();
            
            foreach (var depth in data)
            {
                container.Enqueue(depth);
                if (container.Count == 3)
                {
                    var sum = container.Sum();
                    container.Dequeue();
                    yield return sum;
                }
            }
        }

        public static int CountDepthIncreases(this IEnumerable<int> data)
        {
            var currentDepth = int.MaxValue;
            var depthIncreases = 0;
            foreach (var depth in data)
            {
                if (currentDepth < depth)
                    depthIncreases++;
                currentDepth = depth;
            }

            return depthIncreases;
        }
    }
}