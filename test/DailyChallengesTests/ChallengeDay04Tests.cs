using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day04;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay04Tests
    {
        [Fact]
        public void Should_parse_bingo_game()
        {
            var game = SampleData.AsBingoGame();

            game.Numbers.Should().HaveCountGreaterThan(0);
            game.Boards.Should().HaveCount(3);
            game.Boards.First().Numbers.Should().HaveCount(25);
        }
        
        [Fact]
        public void Should_find_winning_board()
        {
            var game = SampleData.AsBingoGame();

            var (board, number) = game.Boards.FindWinner(game.Numbers);
            
            board.Should().BeEquivalentTo(game.Boards[2]);
            number.Should().Be(24);
            board.CalculateScore().Should().Be(188);
        }
        
        [Fact]
        public void Should_find_loosing_board()
        {
            var game = SampleData.AsBingoGame();

            var (board, number) = game.Boards.FindLooser(game.Numbers);
            
            board.Should().BeEquivalentTo(game.Boards[1]);
            number.Should().Be(13);
            board.CalculateScore().Should().Be(148);
        }

        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
                yield return "";
                yield return "22 13 17 11  0";
                yield return "8  2 23  4 24";
                yield return "21  9 14 16  7";
                yield return "6 10  3 18  5";
                yield return "1 12 20 15 19";
                yield return "";
                yield return "3 15  0  2 22";
                yield return "9 18 13 17  5";
                yield return "19  8  7 25 23";
                yield return "20 11 10 24  4";
                yield return "14 21 16 12  6";
                yield return "";
                yield return "14 21 17 24  4";
                yield return "10 16 15  9 19";
                yield return "18  8 23 26 20";
                yield return "22 11 13  6  5";
                yield return "2  0 12  3  7";
            }
        }
    }
}
