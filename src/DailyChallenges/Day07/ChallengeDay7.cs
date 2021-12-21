using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day07
{
    public class ChallengeDay7 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay7(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 7;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).First().GetCrabSubmarinePositions().ToArray();
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<int> data)
        {
            var (position, fuel) = data.GetOptimalPosition();
            _output.WriteLine($"\tThe optimal crab submarine position is at {position} and requires {fuel} fuel");
        }

        private void ResolvePart2(IEnumerable<int> data)
        {
            var (position, fuel) = data.GetOptimalPosition(CrabSubmarineFuelBurningMethod.CrabTech);
            _output.WriteLine($"\tThe optimal crab submarine position is at {position} and requires {fuel} fuel");
        }
    }

    public static class ChallengeDay7Extensions
    {
        public static IEnumerable<int> GetCrabSubmarinePositions(this string data)
        {
            return data.Split(',').Select(int.Parse);
        }
        
        public static (int position, long fuel) GetOptimalPosition(this IEnumerable<int> data, Func<int, int, long>? fuelMethod = null)
        {
            fuelMethod ??= CrabSubmarineFuelBurningMethod.Original;
            var positions = data.ToArray();
            var max = positions.Max();
            var results = new ConcurrentDictionary<int, long>();

            Parallel.For(0, max, p =>
            {
                long fuel = positions.Select(x => fuelMethod(x, p)).Sum();
                results.TryAdd(p, fuel);
            });

            var (key, value) = results.OrderBy(x => x.Value).First();
            return (key, value);
        }
    }

    public static class CrabSubmarineFuelBurningMethod
    {
        public static long Original(int currentPosition, int targetPosition)
        {
            return Math.Abs(currentPosition - targetPosition);
        }
        
        public static long CrabTech(int currentPosition, int targetPosition)
        {
            var displacement = Math.Abs(currentPosition - targetPosition);
            return Enumerable.Range(1, displacement).Sum();
        }
    }

}