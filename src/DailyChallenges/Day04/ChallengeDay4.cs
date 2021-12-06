using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public static class ChallengeDay4Extensions
    {
        public static BingoGame AsBingoGame(this IEnumerable<string> data)
        {
            using var enumerator = data.GetEnumerator();
            enumerator.MoveNext();
            
            var numbers = enumerator.Current.ReadIntegers();
            var boards = enumerator.GetBoards();

            return new BingoGame
            {
                Numbers = numbers.ToArray(),
                Boards = boards.ToArray()
            };
        }

        public static (BingoBoard, int) FindLooser(this IList<BingoBoard> boards, IEnumerable<int> numbers)
        {
            var remainingBoards = boards.ToList();
            foreach (var (board, number) in boards.FindWinners(numbers))
            {
                remainingBoards.Remove(board);
                if (remainingBoards.Count == 0)
                    return (board, number);
            }


            throw new InvalidOperationException("Failed to find a winner");
        }

        public static (BingoBoard, int) FindWinner(this IList<BingoBoard> boards, IEnumerable<int> numbers)
        {
            return boards.FindWinners(numbers).First();
        }

        private static IEnumerable<(BingoBoard, int)> FindWinners(this IList<BingoBoard> boards, IEnumerable<int> numbers)
        {
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.CheckNumber(number))
                        yield return (board, number);
                }
            }

            throw new InvalidOperationException("Failed to find a winner");
        }

        public static int CalculateScore(this BingoBoard board)
        {
            var score = 0;
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (!board.Matches[y,x])
                    {
                        score += board.Numbers[y,x];
                    }
                }
            }

            return score;
        }

        private static bool CheckNumber(this BingoBoard board, int number)
        {
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    if (board.Numbers[y, x] != number) continue;
                    
                    board.Matches[y,x] = true;
                    return board.HasCompletedLine(y) || board.HasCompletedColumn(x);
                }
            }

            return false;
        }

        private static bool HasCompletedLine(this BingoBoard board, int y)
        {

            var match = true;
            for (var x = 0; x < 5; x++)
            {
                match &= board.Matches[y, x];
            }

            return match;
        }

        private static bool HasCompletedColumn(this BingoBoard board, int x)
        {
            var match = true;
            for (var y = 0; y < 5; y++)
            {
                match &= board.Matches[y, x];
            }

            return match;
        }

        private static IEnumerable<int> ReadIntegers(this string data, char separator = ',')
        {
            return data.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
        }

        private static IEnumerable<BingoBoard> GetBoards(this IEnumerator<string> data)
        {
            int[,] numbers = new int[5, 5];
            var line = 0;

            while (data.MoveNext())
            {
                if (string.IsNullOrEmpty(data.Current))
                {
                    if (line > 0)
                        yield return new BingoBoard(numbers);

                    numbers = new int[5, 5];
                    line = 0;
                    continue;
                }

                var lineNumbers = data.Current.ReadIntegers(' ').ToArray();
                for (var i = 0; i < 5; i++)
                {
                    numbers[line, i] = lineNumbers[i];
                }

                line++;
            }

            if (line > 0)
                yield return new BingoBoard(numbers);
        }
    }

    public class BingoGame
    {
        public BingoGame()
        {
            Numbers = Array.Empty<int>();
            Boards = Array.Empty<BingoBoard>();
        }

        public IEnumerable<int> Numbers { get; init; }
        public BingoBoard[] Boards { get; init; }
    }

    public class BingoBoard
    {
        public BingoBoard(int[,] numbers)
        {
            Numbers = numbers;
            Matches = new bool[5,5];
            // Reset();
        }

        public int[,] Numbers { get; }
        public bool[,] Matches { get; }

        // public void Reset()
        // {
        //     for (var y = 0; y < 5; y++)
        //     {
        //         for (var x = 0; x < 5; x++)
        //         {
        //             Matches[y, x] = false;
        //         }
        //     }
        // }
    }
}