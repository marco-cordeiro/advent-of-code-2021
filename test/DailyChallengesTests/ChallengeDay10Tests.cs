using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DailyChallengesTests
{
    public class ChallengeDay10Tests
    {
        [Fact]
        public void Should_test_day_challenge_logic()
        {
            Assert.True(false, "day challenge not implemented yet");
        }

        private IEnumerable<string> SampleData
        {
            get { yield return "value"; }
        }
        
        [Fact]
        public void IsNull()
        {
            var a = new MyClass();
            
            var r1 = a == null;
            var r2 = a is null;

            r1.Should().Be(true);
            r2.Should().Be(false);
        }
    }

    public class MyClass
    {
        public int Value { get; set; }

        public static bool operator ==(MyClass? a, MyClass? b) => 
            true;

        public static bool operator !=(MyClass? a, MyClass? b)
        {
            return false;
        }
    }
}