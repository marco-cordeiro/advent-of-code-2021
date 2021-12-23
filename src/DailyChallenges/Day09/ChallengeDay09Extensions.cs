using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.DataExtensions;

namespace AdventOfCode2020.DailyChallenges.Day09
{
    public static class ChallengeDay09Extensions
    {
        public static int FindBasinSize(this byte[,] map, int y, int x)
        {
            return map.FindBasinSize(y, x, new List<(int, int)>());
        }

        private static int FindBasinSize(this byte[,] map, int y, int x, List<(int y,int x)> points)
        {
            if (points.Any(p => p.x == x && p.y == y))
                return 0;

            if (map[y, x] == 9)
                return 0;

            points.Add((y,x));            
            if (map[y, x] == 8)
                return 1;

            var yDimension = map.GetLength(0);
            var xDimension = map.GetLength(1);
            
            var size = 0;
            if (y > 0 && map[y - 1, x] >= map[y, x])
                size += map.FindBasinSize(y - 1, x, points);
            if (y < yDimension - 1 && map[y + 1, x] >= map[y, x])
                size += map.FindBasinSize(y + 1, x, points);
            if (x > 0 && map[y, x - 1] >= map[y, x])
                size += map.FindBasinSize(y, x - 1, points);
            if (x < xDimension - 1 && map[y, x + 1] >= map[y, x])
                size += map.FindBasinSize(y, x + 1, points);

            return size + 1;
        }

        public static IEnumerable<(int y, int x)> FindLowPoints(this byte[,] map)
        {
            var yDimension = map.GetLength(0);
            var xDimension = map.GetLength(1);
            for (var y = 0; y < yDimension; y++)
            {
                for (var x = 0; x < xDimension; x++)
                {
                    if ((y <= 0 || map[y - 1, x] > map[y, x]) &&
                        (y >= yDimension - 1 || map[y + 1, x] > map[y, x]) &&
                        (x <= 0 || map[y, x - 1] > map[y, x]) &&
                        (x >= xDimension - 1 || map[y, x + 1] > map[y, x]))
                    {
                        yield return (y, x);
                    }
                }
            }
        }

        public static byte[,] ReadCaveFloorHeightMap(this IEnumerable<string> data) => data.ReadByteMap();
    }
}