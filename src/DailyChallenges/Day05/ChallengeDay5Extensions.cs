using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.DailyChallenges.Day05
{
    public static class ChallengeDay5Extensions
    {
        public static IEnumerable<Line> AsLines(this IEnumerable<string> data)
        {
            return data.Select(AsLine);
        }

        public static IEnumerable<Line> ExcludeDiagonals(this IEnumerable<Line> data)
        {
            return data.Where(l => l.A.X == l.B.X || l.A.Y == l.B.Y);
        }
        
        public static IEnumerable<Line> OnlyDiagonals(this IEnumerable<Line> data)
        {
            return data.Where(l => !(l.A.X == l.B.X || l.A.Y == l.B.Y));
        }

        public static void DrawLines(this byte[,] map, IEnumerable<Line> data)
        {
            foreach (var line in data)
            {
                foreach (var point in line.Points())
                {
                    map[point.X, point.Y]++;
                }
            }
        }

        public static IEnumerable<byte> ToEnumerable(this byte[,] map)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                for (var y = 0; y < map.GetLength(0); y++)
                {
                    yield return map[x, y];
                }
            }
        }

        public static IEnumerable<Point> Points(this Line line)
        {
            if (line.A.X == line.B.X)
            {
                for (var i = Math.Min(line.A.Y, line.B.Y); i <= Math.Max(line.A.Y, line.B.Y); i++)
                {
                    yield return new Point { X = line.A.X, Y = i };
                }
                yield break;
            }
            
            if (line.A.Y == line.B.Y)
            {
                for (var i = Math.Min(line.A.X, line.B.X); i <= Math.Max(line.A.X, line.B.X); i++)
                {
                    yield return new Point { X = i, Y = line.A.Y };
                }
                yield break;
            }
            
            // diagonals
            var xDirection = line.A.X > line.B.X ? -1 : 1;
            var yDirection = line.A.Y > line.B.Y ? -1 : 1;
            var displacement = Math.Max(line.A.X, line.B.X) - Math.Min(line.A.X, line.B.X); 
            for (var i = 0; i <= displacement; i++)
            {
                yield return new Point { X = line.A.X + (i * xDirection), Y = line.A.Y + i  * yDirection };
            }
        }
        
        public static Line AsLine(this string value)
        {
            const string regexExpression = @"(\d{1,3}),(\d{1,3}) -> (\d{1,3}),(\d{1,3})";
            var regex = new Regex(regexExpression);
            var matches = regex.Match(value);

            var x1 = int.Parse(matches.Groups[1].Value);
            var y1 = int.Parse(matches.Groups[2].Value);
            var x2 = int.Parse(matches.Groups[3].Value);
            var y2 = int.Parse(matches.Groups[4].Value);

            return new Line { A = new Point { X = x1, Y = y1 }, B = new Point { X = x2, Y = y2 } };
        }
    }
}