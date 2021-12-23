using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.DailyChallenges.DataExtensions
{
    public static class DataExtensions
    {
        public static byte[,] ReadByteMap(this IEnumerable<string> data)
        {
            var materialisedData = data.ToArray();
            var map = new byte[materialisedData.Length, materialisedData[0].Length];
            var y = 0;
            foreach (var line in materialisedData)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    map[y,x] = byte.Parse(line[x].ToString());
                }

                y++;
            }

            return map;
        }
    }
}