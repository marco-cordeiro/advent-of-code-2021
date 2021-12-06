using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.DailyChallenges.Day05;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DailyChallengesTests
{
    public class ChallengeDay05Tests
    {
        private readonly ITestOutputHelper _output;

        public ChallengeDay05Tests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Should_convert_data_to_line()
        {
            "0,9 -> 5,9".AsLine().Should().BeEquivalentTo(new Line { A = new Point { X = 0, Y = 9 }, B = new Point { X = 5, Y = 9 } });
        }
        
        [Fact]
        public void Should_filter_diagonal_lines()
        {
            var notDiagonals = SampleData.AsLines().ExcludeDiagonals().ToArray();

            notDiagonals.Should().HaveCount(6);
        }

        [Fact]
        public void Should_points_in_horizontal_lines()
        {
            var line = new Line { A = new Point { X = 0, Y = 9 }, B = new Point { X = 5, Y = 9 } };

            var points = line.Points();
            points.Should().HaveCount(6);
        }

        [Fact]
        public void Should_points_in_vertical_lines()
        {
            var line = new Line { A = new Point { X = 9, Y = 0 }, B = new Point { X = 9, Y = 5 } };

            var points = line.Points();
            points.Should().HaveCount(6);
        }

        [Fact]
        public void Should_count_all_line_overlaps()
        {
            var lines = SampleData.AsLines().ExcludeDiagonals().ToArray();
            var map = new byte[10, 10];
            map.DrawLines(lines);
            
            var values = map.ToEnumerable().Count(x => x > 1);
            values.Should().Be(5);
        }

        [Fact]
        public void Should_draw_lines_in_map()
        {
            var lines = SampleData.AsLines().ToArray();
            var map = new byte[10, 10];

            map.DrawLines(lines);

            for (var y = 0; y <= 9; y++)
            {
                var sb = new StringBuilder();
                for (var x = 0; x <= 9; x++)
                {
                    var value = map[x, y];
                    sb.Append(value > 0 ? value.ToString()[0] : '.');
                }
                _output.WriteLine(sb.ToString());
            }
        }

        private IEnumerable<string> SampleData
        {
            get
            {
                yield return "0,9 -> 5,9";
                yield return "8,0 -> 0,8";
                yield return "9,4 -> 3,4";
                yield return "2,2 -> 2,1";
                yield return "7,0 -> 7,4";
                yield return "6,4 -> 2,0";
                yield return "0,9 -> 2,9";
                yield return "3,4 -> 1,4";
                yield return "0,0 -> 8,8";
                yield return "5,5 -> 8,2";
            }
        }
    }
}
