using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.DailyChallenges.Day08
{
    public static class ChallengeDay08Extensions
    {
        public static IEnumerable<DisplayData> ReadSubmarineDisplayData(this IEnumerable<string> data)
        {
            return data.Select(sample => sample.Split('|')).Select(parts => new DisplayData
            {
                Signal = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray(),
                Output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray()
            });
        }

        /// <summary>
        /// 1 = 2 digits
        /// 7 = 3 digits
        /// 4 = 4 digits
        /// 8 = 7 digits
        ///
        /// 00000
        /// 1   2
        /// 1   2
        /// 33333
        /// 4   5
        /// 4   5
        /// 66666
        ///
        ///  0 - 012456
        ///  1 - 25
        ///  2 - 02346
        ///  3 - 02356
        ///  4 - 1235
        ///  5 - 01356
        ///  6 - 013456
        ///  7 - 025
        ///  8 - 0123456
        ///  9 - 012356
        /// </summary>
        public static DisplayDataDecoder Analyze(this DisplayData data)
        {
            var samples = data.Signal;
            var patterns = new string[10];
            patterns[1] = samples.First(x => x.Length == 2);
            patterns[4] = samples.First(x => x.Length == 4);
            patterns[7] = samples.First(x => x.Length == 3);
            patterns[8] = samples.First(x => x.Length == 7);

            samples.Where(x => x.Length == 5).FiveDigitAnalyser(patterns);
            samples.Where(x => x.Length == 6).SixDigitAnalyser(patterns);

            return new DisplayDataDecoder(patterns);
        }

        private static void SixDigitAnalyser(this IEnumerable<string> values, string[] patterns)
        {
            var valueArray = values.ToArray();
            patterns[0] = valueArray.Single(x => x.Intersect(patterns[5]).Count() == 4);
            patterns[9] = valueArray.Single(x => !x.Equals(patterns[0]) && x.Intersect(patterns[1]).Count() == 2);
            patterns[6] = valueArray.Single(x => !x.Equals(patterns[0]) && !x.Equals(patterns[9]));
        }

        private static void FiveDigitAnalyser(this IEnumerable<string> values, string[] patterns)
        {
            var valueArray = values.ToArray();

            var digitsOneAndThree = new string(patterns[4].Where(c => !patterns[1].Contains(c)).ToArray());
            patterns[3] = valueArray.Single(x => new string(x.Intersect(patterns[1]).ToArray()).Equals(patterns[1]));
            patterns[5] = valueArray.Single(x => new string(x.Intersect(digitsOneAndThree).ToArray()).Equals(digitsOneAndThree));
            patterns[2] = valueArray.Single(x => !x.Equals(patterns[3]) && !x.Equals(patterns[5]));
        }
    }
}