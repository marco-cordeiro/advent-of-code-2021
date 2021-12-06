using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay05Tests
    {
        [Fact]
        public void Should_test_day_challenge_logic()
        {
            Assert.True(false, "day challenge not implemented yet");
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
