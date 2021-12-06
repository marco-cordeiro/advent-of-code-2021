using System.IO;
using System.Reflection.Metadata.Ecma335;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day04
{
    public class ChallengeDay4 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay4(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 4;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var game = _dataProvider.Read(Day).AsBingoGame();

            ResolvePart1(game);
            ResolvePart2(game);
        }

        private void ResolvePart1(BingoGame game)
        {
            var (board, number) = game.Boards.FindWinner(game.Numbers);

            var score = board.CalculateScore();
            _output.WriteLine($"\tThe score of the winning board is {score * number} and the last drawn number will be {number}");
        }

        private void ResolvePart2(BingoGame game)
        {
            var (board, number) = game.Boards.FindLooser(game.Numbers);
            
            var score = board.CalculateScore();
            _output.WriteLine($"\tThe score of the loosing board is {score * number} and the last drawn number will be {number}");
        }
    }
}