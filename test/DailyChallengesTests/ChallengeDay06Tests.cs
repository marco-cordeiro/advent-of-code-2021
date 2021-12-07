using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day06;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay06Tests
    {
        [Fact]
        public void Population_should_evolve()
        {
            var population = SampleData.First().AsLanternFishPopulation().ToList();
            
            foreach (var state in ExpectedEvolution)
            {
                population.Evolve();
                population.Should().BeEquivalentTo(state.AsLanternFishPopulation());
            }
        }
        
        [Fact]
        public void Population_should_evolve_80_days()
        {
            var population = SampleData.First().AsLanternFishPopulation().ToList();
            
            population.Evolve(80);

            population.Count.Should().Be(5934);
        }
        
        [Fact]
        public void Population_should_evolve_80_days_two()
        {
            var data = SampleData.First().AsLanternFishPopulation();
            var population = new LanternFishPopulation(data);
            
            population.Evolve(80);
            
            population.PopulationCount.Should().Be(5934);
        }
        
        [Fact]
        public void Population_should_evolve_18_days_two()
        {
            var data = SampleData.First().AsLanternFishPopulation();
            var population = new LanternFishPopulation(data);
            
            population.Evolve(18);
            
            population.PopulationCount.Should().Be(26);
        }
        
        [Fact]
        public void Population_should_evolve_256_days()
        {
            var data = SampleData.First().AsLanternFishPopulation();
            var population = new LanternFishPopulation(data);
            
            population.Evolve(256);
            
            population.PopulationCount.Should().Be(26984457539);
        }

        private IEnumerable<string> SampleData
        {
            get { yield return "3,4,3,1,2"; }
        }

        private IEnumerable<string> ExpectedEvolution
        {
            get
            {
                yield return "2,3,2,0,1";
                yield return "1,2,1,6,0,8";
                yield return "0,1,0,5,6,7,8";
                yield return "6,0,6,4,5,6,7,8,8";
                yield return "5,6,5,3,4,5,6,7,7,8";
                yield return "4,5,4,2,3,4,5,6,6,7";
                yield return "3,4,3,1,2,3,4,5,5,6";
                yield return "2,3,2,0,1,2,3,4,4,5";
                yield return "1,2,1,6,0,1,2,3,3,4,8";
                yield return "0,1,0,5,6,0,1,2,2,3,7,8";
                yield return "6,0,6,4,5,6,0,1,1,2,6,7,8,8,8";
                yield return "5,6,5,3,4,5,6,0,0,1,5,6,7,7,7,8,8";
                yield return "4,5,4,2,3,4,5,6,6,0,4,5,6,6,6,7,7,8,8";
                yield return "3,4,3,1,2,3,4,5,5,6,3,4,5,5,5,6,6,7,7,8";
                yield return "2,3,2,0,1,2,3,4,4,5,2,3,4,4,4,5,5,6,6,7";
                yield return "1,2,1,6,0,1,2,3,3,4,1,2,3,3,3,4,4,5,5,6,8";
                yield return "0,1,0,5,6,0,1,2,2,3,0,1,2,2,2,3,3,4,4,5,7,8";
                yield return "6,0,6,4,5,6,0,1,1,2,6,0,1,1,1,2,2,3,3,4,6,7,8,8,8,8";
            }
        }

    }
}
