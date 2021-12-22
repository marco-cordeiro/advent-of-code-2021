using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day09;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay09Tests
    {
        [Fact]
        public void Should_read_heightmap()
        {
            var map = SampleData.ReadCaveFloorHeightMap();
            map.Should().HaveCount(50);
        }
        
        [Fact]
        public void Should_list_of_low_points()
        {
            var map = SampleData.ReadCaveFloorHeightMap();
            var lowPoints = map.FindLowPoints().ToArray();
            lowPoints.Should().HaveCount(4);
            lowPoints.Sum(x => map[x.y, x.x] + 1).Should().Be(15);
        }

        [Theory]
        [InlineData(0, 9, 9)]
        [InlineData(2, 2, 14)]
        [InlineData(4, 6, 9)]
        public void Should_calculate_basin_size(int y, int x, int expectedSize)
        {
            var map = SampleData.ReadCaveFloorHeightMap();
            var basinSize = map.FindBasinSize(y, x);
            basinSize.Should().Be(expectedSize);
        }

        [Fact]
        public void Should_calculate_basin_sizes()
        {
            var map = SampleData.ReadCaveFloorHeightMap();
            var lowPoints = map.FindLowPoints().ToArray();
            var basinSizes = lowPoints.Select(x => map.FindBasinSize(x.y, x.x)).ToArray();
            var result = basinSizes
                .OrderByDescending(x => x)
                .Take(3)
                .Aggregate((t, s) => t * s);
            result.Should().Be(1134);
        }

        private IEnumerable<string> SampleData
        {
            get { 
                yield return "2199943210";
                yield return "3987894921";
                yield return "9856789892";
                yield return "8767896789";
                yield return "9899965678"; 
            }
        }
    }
}