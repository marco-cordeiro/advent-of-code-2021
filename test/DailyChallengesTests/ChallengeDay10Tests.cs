using System.Collections.Generic;
using AdventOfCode2020.DailyChallenges.Day10;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay10Tests
    {
        [Fact]
        public void Should_find_syntax_error()
        {
            var code = InvalidSampleData;
            var result = code.IsSyntaxValid();
            result.IsValid.Should().Be(false);
            result.UnexpectedChar.Should().Be('}');
        }
        
        [Fact]
        public void Should_ignore_incomplete_code()
        {
            var code = IncompleteSampleData;
            var result = code.IsSyntaxValid();
            result.IsValid.Should().Be(true);
            result.AutoComplete.Should().Be("}}]])})]");
            result.UnexpectedChar.Should().Be('\0');
        }
        
        [Fact]
        public void Should_account_syntax_errors_score()
        {
            var errors = SampleData.CheckCodeSyntax();
            var result = errors.ScoreSyntaxErrors();
            
            result.Should().Be(26397);
        }
        
        [Fact]
        public void Should_account_autocomplete_score()
        {
            var status = new SyntaxStatus("])}>");
            var result =status.ScoreSyntaxAutoComplete();
            
            result.Should().Be(294);
        }
        
        [Fact]
        public void Should_account_autocomplete_score2()
        {
            var code = IncompleteSampleData;
            var result = code.IsSyntaxValid().ScoreSyntaxAutoComplete();
            result.Should().Be(288957);
        }
        
        [Fact]
        public void Should_account_autocomplete_scores()
        {
            var status = SampleData.CheckCodeSyntax();
            var result = status.ScoreAutoComplete();
            
            result.Should().Be(288957);
        }

        private string IncompleteSampleData => "[({(<(())[]>[[{[]{<()<>>";
        private string InvalidSampleData => "{([(<{}[<>[]}>{[]{[(<()>";
        private IEnumerable<string> SampleData
        {
            get { 
                yield return "[({(<(())[]>[[{[]{<()<>>";
                yield return "[(()[<>])]({[<{<<[]>>(";
                yield return "{([(<{}[<>[]}>{[]{[(<()>";
                yield return "(((({<>}<{<{<>}{[]{[]{}";
                yield return "[[<[([]))<([[{}[[()]]]";
                yield return "[{[{({}]{}}([{[{{{}}([]";
                yield return "{<[[]]>}<{[{[{[]{()[[[]";
                yield return "[<(<(<(<{}))><([]([]()";
                yield return "<{([([[(<>()){}]>(<<{{";
                yield return "<{([{{}}[<[[[<>{}]]]>[]]";
            }
        }
    }
}