using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day07;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay07Tests
    {
        [Fact]
        public void Should_calculate_optimal_position_and_fuel_cost()
        {
            var data = SampleData.First().GetCrabSubmarinePositions();
            var (position, fuel) = data.GetOptimalPosition();
            position.Should().Be(2);
            fuel.Should().Be(37);
        }
        
        [Fact]
        public void Should_calculate_optimal_position_and_fuel_cost_using_crab_tech()
        {
            var data = SampleData.First().GetCrabSubmarinePositions();
            var (position, fuel) = data.GetOptimalPosition(CrabSubmarineFuelBurningMethod.CrabTech);
            position.Should().Be(5);
            fuel.Should().Be(168);
        }

        private IEnumerable<string> SampleData
        {
            get { yield return "16,1,2,0,4,2,7,1,2,14"; }
        }
    }
}

