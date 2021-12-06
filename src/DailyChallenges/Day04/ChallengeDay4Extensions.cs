using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.DailyChallenges.Day04
{
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
}