using System.Collections.Generic;
using AdventOfCode2020.DailyChallenges.DataExtensions;

namespace AdventOfCode2020.DailyChallenges.Day11
{
    public static  class ChallengeDay11Extensions
    {
        public static byte[,] ReadOctopusMap(this IEnumerable<string> data) => data.ReadByteMap();

        public static int ExecuteSteps(this byte[,] map, int steps)
        {
            var stepCount = 0;
            for (var i = 0; i < steps; i++)
            {
                stepCount += map.ExecuteStep();
            }

            return stepCount;
        }
        
        public static int ExecuteStepsUntilAllOctopusesFlash(this byte[,] map)
        {
            var steps = 0;
            do
            {
                steps++;
                var count = map.ExecuteStep();
                if (count == 100)
                    break;
            } while (true);

            return steps;
        }

        public static int ExecuteStep(this byte[,] octopusMap)
        {
            var map = new MapWrapper(octopusMap);

            for (var y = 0; y < map.DimensionY; y++)
            {
                for (var x = 0; x < map.DimensionX; x++)
                {
                    map.Increase(y, x);
                }
            }

            for (var y = 0; y < map.DimensionY; y++)
            {
                for (var x = 0; x < map.DimensionX; x++)
                {
                    if (octopusMap[y, x] > 9)
                        octopusMap[y, x] = 0;
                }
            }

            return map.Flashes;
        }

        private class MapWrapper
        {
            private readonly byte[,] _map;
            private readonly bool[,] _flashed;

            public MapWrapper(byte [,] map)
            {
                DimensionY = map.GetLength(0);
                DimensionX = map.GetLength(1);

                _map = map;
                _flashed = new bool[map.GetLength(0),map.GetLength(1)];
            }

            public int DimensionX { get; }
            public int DimensionY { get; }
            public int Flashes { get; private set; }

            public void Increase(int y, int x)
            {
                _map[y, x]++;
                if (_map[y, x] <= 9 || _flashed[y, x]) return;

                _flashed[y, x] = true;
                Flashes++;
                if (y > 0)
                {
                    if (x > 0)
                        Increase(y - 1, x - 1);
                    Increase(y - 1, x);
                    if (x < DimensionX - 1)
                        Increase(y - 1, x + 1);
                }
                if (y < DimensionY - 1)
                {
                    if (x > 0)
                        Increase(y + 1, x - 1);
                    Increase(y + 1, x);
                    if (x < DimensionX - 1)
                        Increase(y + 1, x + 1);
                }
                if (x > 0)
                    Increase(y, x - 1);
                if (x < DimensionX - 1)
                    Increase(y, x + 1);
            }
        }
    }
}