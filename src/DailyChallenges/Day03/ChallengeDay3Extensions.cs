using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.DailyChallenges.Day03
{
    public static class ChallengeDay3Extensions
    {
        private const int BIT_PRECISION = 12;
        
        public static IEnumerable<BitArray> AsBinary(this IEnumerable<string> data)
        {
            return data.Select(value => AsBinary((string)value));
        }

        public static int GetOxygenGeneratorRating(this IEnumerable<BitArray> data)
        {
            List<BitArray>? rates = null;
            for (var i = 0; i < BIT_PRECISION; i++)
            {
                rates = FilterRatings(rates ?? data, true, i);

                if (rates.Count == 1)
                    return rates.First().ToInt();
            }

            throw new InvalidOperationException("Failed to find the Oxygen Generator Rating");
        }
        
        public static int GetCO2ScrubberRating(this IEnumerable<BitArray> data)
        {
            List<BitArray>? rates = null;
            for (var i = 0; i < BIT_PRECISION; i++)
            {
                rates = FilterRatings(rates ?? data, false, i);

                if (rates.Count == 1)
                    return rates.First().ToInt();
            }

            throw new InvalidOperationException("Failed to find the CO2 scrubber Rating");
        }
        
        private static List<BitArray> FilterRatings(this IEnumerable<BitArray> data, bool usePrevalent, int position = 0)
        {
            var count = 0;
            var bitCount = 0;
            var ones = new List<BitArray>(); 
            var zeros = new List<BitArray>(); 
            
            foreach (var value in data)
            {
                var bit = value[BIT_PRECISION - 1 - position];
                switch (bit)
                {
                    case true:
                        bitCount++;
                        ones.Add(value);
                        break;
                    case false:
                        zeros.Add(value);
                        break;
                }

                count++;
            }

            var prevalent = bitCount >= count / 2.0 ? 1 : 0;

            return bitCount > 0 && (prevalent == 1 && usePrevalent || prevalent == 0 && !usePrevalent) ? ones : zeros;
        }

        public static BitArray AsBinary(this string data)
        {
            return new BitArray(new [] { Convert.ToInt32(data, 2) });
        }
        
        public static (int gamma, int epsilon) GetRates(this IEnumerable<BitArray> data)
        {
            var bitCount = new int[BIT_PRECISION];
            var count = 0;
            foreach (var bitwise in data)
            {
                for (var i = 0; i < BIT_PRECISION; i++)
                {
                    if (bitwise[i])
                        bitCount[i]++;
                }

                count++;
            }

            var gamma = new BitArray(BIT_PRECISION);
            var epsilon = new BitArray(BIT_PRECISION);
            
            for (var i = 0; i < BIT_PRECISION; i++)
            {
                var prevalent = bitCount[i] > count / 2;
                gamma[i] = bitCount[i] > 0  && prevalent;
                epsilon[i] = bitCount[i] > 0 && !prevalent;
            }

            return (gamma.ToInt(), epsilon.ToInt());
        }

        private static int ToInt(this BitArray value)
        {
            int[] array = new int[1];
            value.CopyTo(array, 0);
            return array[0];
        }
    }
}