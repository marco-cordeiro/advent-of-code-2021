using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day06
{
    public class ChallengeDay6 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay6(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 6;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).First().AsLanternFishPopulation().ToArray();
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<int> data)
        {
            var population = new LanternFishPopulation(data);
            
            population.Evolve(80);
            
            _output.WriteLine($"\tThere should be {population.PopulationCount} lantern fish after 80 days");
        }

        private void ResolvePart2(IEnumerable<int> data)
        {
            var population = new LanternFishPopulation(data);
            
            population.Evolve(256);
            
            _output.WriteLine($"\tThere should be {population.PopulationCount} lantern fish after 256 days");
        }

    }

    public class LanternFishPopulation
    {
        private Dictionary<int, long> _population;

        public LanternFishPopulation(IEnumerable<int> population)
        {
            _population = population.ToLookup(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
        }

        public long PopulationCount => _population.Sum(x=>x.Value);

        public void Evolve(int days)
        {
            for (var i = 0; i < days; i++)
            {
                Evolve();
            }
        }

        public void Evolve()
        {
            var nextDayPopulation = _population.ToDictionary(x => x.Key - 1, x => x.Value);
            nextDayPopulation.TryGetValue(-1, out var expectantSpecimen);
            nextDayPopulation.TryGetValue(6, out var daySixSpecimen);
            nextDayPopulation.Remove(-1);
            nextDayPopulation[6] = daySixSpecimen + expectantSpecimen;
            nextDayPopulation[8] = expectantSpecimen;

            _population = nextDayPopulation;
        }
    }
    
    public static class ChallengeDay6Extensions
    {
        public static IEnumerable<int> AsLanternFishPopulation(this string data)
        {
            return data.Split(',').Select(int.Parse);
        }

        public static void Evolve(this IList<int> population, int days)
        {
            for (var i = 0; i < days; i++)
            {
                population.Evolve();
            }
        }

        public static void Evolve(this IList<int> population)
        {
            var currentPopulation = population.Count;
            var newPopulation = new ConcurrentBag<byte>();
                
            Parallel.For(0, currentPopulation, i =>
            {
                var specimen = population[i]--;
                if (specimen == 0)
                {
                    population[i] = 6;
                    newPopulation.Add(8);
                }
            });

            foreach (var specimen in newPopulation)
            {
                population.Add(specimen);    
            }
        }
    }
}