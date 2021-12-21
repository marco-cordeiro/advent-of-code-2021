using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode2020.DailyChallenges.Day06
{
    public static class Test
    {
        public static bool IsNull(this MyClass? a)
        {
            var r1 = a == null;
            var r2 = a is null;

            return r1 && r2;
        }
    }

    public class MyClass
    {
        public int Value { get; set; }

        public static MyClass operator +(MyClass a) => new () { Value = a.Value + 1 };
        public static MyClass operator +(MyClass a, MyClass b) => new () { Value = a.Value + b.Value };
        public static MyClass operator -(MyClass a) => new () { Value = a.Value - 1 };
        public static MyClass operator -(MyClass a, MyClass b) => new () { Value = a.Value - b.Value };
        public static bool operator ==(MyClass? a, MyClass? b) => a?.Value == b?.Value ;
        public static bool operator !=(MyClass? a, MyClass? b) => a?.Value == b?.Value ;
    }
}