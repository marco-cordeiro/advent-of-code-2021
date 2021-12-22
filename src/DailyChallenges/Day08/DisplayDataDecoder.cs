using System.Collections.Generic;

namespace AdventOfCode2020.DailyChallenges.Day08
{
    public class DisplayDataDecoder
    {
        private Dictionary<string,int> _data;

        public DisplayDataDecoder(string[] patterns)
        {
            _data = new Dictionary<string, int>();
            for (var i = 0; i < patterns.Length; i++)
            {
                _data[patterns[i]] = i;
            }
        }

        public int Decode(IEnumerable<string> data)
        {
            var multiplier = 1000;
            var value = 0;
            foreach (var digit in data)
            {
                value += _data[digit] * multiplier;
                multiplier /= 10;
            }

            return value;
        }
    }
}