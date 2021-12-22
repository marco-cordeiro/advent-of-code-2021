using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2020.Framework;
using DataProvider;

namespace AdventOfCode2020.DailyChallenges.Day10
{
    public class ChallengeDay10 : IAdventCodeDayChallenge
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly TextWriter _output;

        public ChallengeDay10(IDataProvider<string> dataProvider, TextWriter output)
        {
            _dataProvider = dataProvider;
            _output = output;
        }

        public int Day => 10;

        public void Execute()
        {
            _output.WriteLine($"Advent of Code day {Day}");

            var data = _dataProvider.Read(Day).ToArray();
            
            ResolvePart1(data);
            ResolvePart2(data);
        }

        private void ResolvePart1(IEnumerable<string> code)
        {
            var errors = code.CheckCodeSyntax();
            var result = errors.ScoreSyntaxErrors();
            _output.WriteLine($"\tThe score of all syntax errors is {result}");
        }

        private void ResolvePart2(IEnumerable<string> code)
        {
            var errors = code.CheckCodeSyntax();
            var result = errors.ScoreAutoComplete();
            _output.WriteLine($"\tThe score of auto complete code is {result}");
        }
    }
    
    public static class ChallengeDay10Extensions
    {
        public static int ScoreSyntaxErrors(this IEnumerable<SyntaxStatus> errors)
        {
            var scoreMap = new Dictionary<char, int>
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137,
            };
            return errors.Where(x => !x.IsValid).Sum(x => scoreMap[x.UnexpectedChar]);
        }

        public static long ScoreSyntaxAutoComplete(this SyntaxStatus status)
        {
            var scoreMap = new Dictionary<char, int>
            {
                [')'] = 1,
                [']'] = 2,
                ['}'] = 3,
                ['>'] = 4
            };
            long score = 0;
            foreach (var @char in status.AutoComplete)
            {
                score *= 5;
                score += scoreMap[@char];
            }

            return score;
        }
        
        public static long ScoreAutoComplete(this IEnumerable<SyntaxStatus> status)
        {
            var code = status.Where(x => x.IsValid && !string.IsNullOrEmpty(x.AutoComplete));
            var scores = code.Select(x => x.ScoreSyntaxAutoComplete()).OrderBy(x => x).ToArray();
            return scores[scores.Length / 2];
        }

        public static IEnumerable<SyntaxStatus> CheckCodeSyntax(this IEnumerable<string> code)
        {
            return code.Select(line => line.IsSyntaxValid());
        }

        public static SyntaxStatus IsSyntaxValid(this string code)
        {
            var map = new Dictionary<char, char>
            {
                ['<'] = '>',
                ['{'] = '}',
                ['('] = ')',
                ['['] = ']',
            };
            var blocks = new Stack<char>();
            foreach (var @char in code)
            {
                switch (@char)
                {
                    case '<':
                    case '(':
                    case '{':
                    case '[':
                        blocks.Push(@char);
                        break;
                    case '>':
                    case ')':
                    case '}':
                    case ']':
                        var expectedChar = blocks.Pop();
                        if (@char != map[expectedChar])
                        {
                            return new SyntaxStatus(@char);
                        }
                        break;
                    default:
                        throw new ArgumentException(@char.ToString());
                }
            }

            var autoComplete = new string(blocks.ToArray().Select(x=>map[x]).ToArray());
            return new SyntaxStatus(autoComplete);
        }
    }

    public class SyntaxStatus
    {
        public SyntaxStatus()
        {
            IsValid = true;
            UnexpectedChar = '\0';
            AutoComplete = string.Empty;
        }
        
        public SyntaxStatus(string autoComplete)
        {
            IsValid = true;
            UnexpectedChar = '\0';
            AutoComplete = autoComplete;
        }
        
        public SyntaxStatus(char unexpectedChar)
        {
            IsValid = false;
            UnexpectedChar = unexpectedChar;
            AutoComplete = string.Empty;
        }

        public bool IsValid { get; }
        public char UnexpectedChar { get; }
        public string AutoComplete { get; }
    }

}