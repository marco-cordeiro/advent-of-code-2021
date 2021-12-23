using System.Collections.Generic;
using AdventOfCode2020.DailyChallenges.Day11;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay11Tests
    {
        [Fact]
        public void Should_read_map()
        {
            var map = SampleData.ReadOctopusMap();

            map.GetLength(0).Should().Be(10);
            map.GetLength(1).Should().Be(10);
        }

        [Fact]
        public void Should_step_octopus_map_10_times()
        {
            var map = SampleData.ReadOctopusMap();
            var flashes = map.ExecuteSteps(10);
            flashes.Should().Be(204);
        }

        [Fact]
        public void Should_step_octopus_map_100_times()
        {
            var map = SampleData.ReadOctopusMap();
            var flashes = map.ExecuteSteps(100);
            flashes.Should().Be(1656);
        }

        [Fact]
        public void Should_step_count_steps_until_all_octopuses_flash()
        {
            var map = SampleData.ReadOctopusMap();
            var flashes = map.ExecuteStepsUntilAllOctopusesFlash();
            flashes.Should().Be(195);
        }

        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "5483143223";
                yield return "2745854711";
                yield return "5264556173";
                yield return "6141336146";
                yield return "6357385478";
                yield return "4167524645";
                yield return "2176841721";
                yield return "6882881134";
                yield return "4846848554";
                yield return "5283751526";
            }
        }
    }
}