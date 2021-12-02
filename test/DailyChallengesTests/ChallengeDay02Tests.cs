using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day02;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay02Tests
    {
        [Fact]
        public void Should_convert_data_to_movements()
        {
            "forward 5".ConvertToMovement().Should()
                .BeEquivalentTo(new Movement { Direction = Direction.Forward, Offset = 5 });
            "down 4".ConvertToMovement().Should()
                .BeEquivalentTo(new Movement { Direction = Direction.Down, Offset = 4 });
            "up 11".ConvertToMovement().Should()
                .BeEquivalentTo(new Movement { Direction = Direction.Up, Offset = 11 });
        }
        
        [Fact]
        public void Should_move_to_expected_location()
        {
            var (depth, hPosition) = SampleData.Select(x => x.ConvertToMovement()).Move(false);

            depth.Should().Be(10);
            hPosition.Should().Be(15);
        }
        
        [Fact]
        public void Should_move_using_aim_to_expected_location()
        {
            var (depth, hPosition) = SampleData.Select(x => x.ConvertToMovement()).Move(true);

            depth.Should().Be(60);
            hPosition.Should().Be(15);
        }
        
        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "forward 5";
                yield return "down 5";
                yield return "forward 8";
                yield return "up 3";
                yield return "down 8";
                yield return "forward 2";
            }
        }
    }
}
