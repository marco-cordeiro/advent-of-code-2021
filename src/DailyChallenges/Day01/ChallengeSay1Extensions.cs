using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.DailyChallenges.Day01
{
    public static class ChallengeSay1Extensions
    {
        public static IEnumerable<int> AsThreeMeasurementWindows(this IEnumerable<int> data)
        {
            var container = new Queue<int>();
            
            foreach (var depth in data)
            {
                container.Enqueue(depth);
                if (container.Count != 3) continue;
                var sum = container.Sum();
                container.Dequeue();
                yield return sum;
            }
        }

        public static int CountDepthIncreases(this IEnumerable<int> data)
        {
            var currentDepth = int.MaxValue;
            var depthIncreases = 0;
            foreach (var depth in data)
            {
                if (currentDepth < depth)
                    depthIncreases++;
                currentDepth = depth;
            }

            return depthIncreases;
        }
    }
}