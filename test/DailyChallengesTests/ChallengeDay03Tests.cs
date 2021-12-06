using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AdventOfCode2020.DailyChallenges.Day03;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay03Tests
    {
        [Fact]
        public void Should_convert_binary_string_to_integer()
        {
            var result = "00100".AsBinary();

            result.Should().BeEquivalentTo(new BitArray(new[] { 4 }));
        }
        
        [Fact]
        public void Should_get_the_rates()
        {
            var data = SampleData.AsBinary();
            var (gamma, epsilon) = data.GetRates();

            gamma.Should().Be(22);
            epsilon.Should().Be(9);
        }
        
        [Fact]
        public void Should_get_the_oxygen_generator_rating()
        {
            var data = SampleData.AsBinary();
            var rating = data.GetOxygenGeneratorRating();

            rating.Should().Be(23);
        }
        
        [Fact]
        public void Should_get_the_co2_scrubber_rating()
        {
            var data = SampleData.AsBinary();
            var rating = data.GetCO2ScrubberRating();

            rating.Should().Be(10);
        }
        
        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "00100";
                yield return "11110";
                yield return "10110";
                yield return "10111";
                yield return "10101";
                yield return "01111";
                yield return "00111";
                yield return "11100";
                yield return "10000";
                yield return "11001";
                yield return "00010";
                yield return "01010";
            }
        }
    }
}
