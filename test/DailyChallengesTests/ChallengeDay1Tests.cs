using System.Collections.Generic;
using System.IO;
using System.Text;
using AdventOfCode2020.DailyChallenges.Day01;
using DataProvider;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace DailyChallengesTests
{
    public class ChallengeDay1Tests
    {
        [Fact]
        public void Should_count_number_of_depth_increases()
        {
            var result = SampleData.CountDepthIncreases();

            result.Should().Be(7);
        }
        
        [Fact]
        public void Should_count_number_of_depth_increases_in_three_measurement_windows()
        {
            var result = SampleData.AsThreeMeasurementWindows().CountDepthIncreases();

            result.Should().Be(5);
        }
        
        private IEnumerable<int> SampleData
        {
            get
            {
                yield return 199;
                yield return 200;
                yield return 208;
                yield return 210;
                yield return 200;
                yield return 207;
                yield return 240;
                yield return 269;
                yield return 260;
                yield return 263;
            }
        }
    }
}
