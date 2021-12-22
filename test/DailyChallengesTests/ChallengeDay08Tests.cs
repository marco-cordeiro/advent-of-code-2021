using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.DailyChallenges.Day08;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay08Tests
    {
        [Fact]
        public void Should_count_unique_digits()
        {
            var data = SampleData.ReadSubmarineDisplayData();
            var digits = data.SelectMany(x => x.Output).Count(x => x.Length is 2 or 3 or 4 or 7);
            digits.Should().Be(26);
        }

        [Fact]
        public void Should_decode_digits()
        {
            var data = SampleData2.ReadSubmarineDisplayData().First();
            var decoder = data.Analyze();
            var result = decoder.Decode(data.Output);
            result.Should().Be(5353);
        }
        
        [Fact]
        public void Should_sum_output_values()
        {
            var data = SampleData.ReadSubmarineDisplayData();
            var result = data.Select(x=>x.Analyze().Decode(x.Output)).Sum();
            result.Should().Be(61229);
        }

        /// <summary>
        /// 1 = 2 digits
        /// 7 = 3 digits
        /// 4 = 4 digits
        /// 8 = 7 digits
        /// </summary>
        private IEnumerable<string> SampleData2
        {
            get { yield return "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"; }
        }
        
        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe";
                yield return "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc";
                yield return "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg";
                yield return "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb";
                yield return "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea";
                yield return "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb";
                yield return "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe";
                yield return "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef";
                yield return "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb";
                yield return "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";
            }
        }
    }
}