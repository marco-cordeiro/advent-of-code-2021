namespace AdventOfCode2020.DailyChallenges.Day04
{
    public class BingoBoard
    {
        public BingoBoard(int[,] numbers)
        {
            Numbers = numbers;
            Matches = new bool[5,5];
        }

        public int[,] Numbers { get; }
        public bool[,] Matches { get; }
    }
}