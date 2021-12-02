using System;
using System.Collections.Generic;

namespace AdventOfCode2020.DailyChallenges.Day02
{
    public static class ChallengeDay2Extensions
    {
        public static (int depth, int hPosition) Move(this IEnumerable<Movement> movements, bool useAim)
        {
            return useAim
                ? movements.MoveUsingAim()
                : movements.MoveWithoutAim();
        }

        private static (int depth, int hPosition) MoveWithoutAim(this IEnumerable<Movement> movements)
        {
            var depth = 0;
            var hPosition = 0;

            foreach (var movement in movements)
            {
                switch (movement.Direction)
                {
                    case Direction.Forward:
                        hPosition += movement.Offset;
                        break;
                    case Direction.Up:
                        depth -= movement.Offset;
                        break;
                    case Direction.Down:
                        depth += movement.Offset;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return (depth, hPosition);
        }
        
        private static (int depth, int hPosition) MoveUsingAim(this IEnumerable<Movement> movements)
        {
            var depth = 0;
            var hPosition = 0;
            var aim = 0;

            foreach (var movement in movements)
            {
                switch (movement.Direction)
                {
                    case Direction.Forward:
                        hPosition += movement.Offset;
                        depth += movement.Offset * aim;
                        break;
                    case Direction.Up:
                        aim -= movement.Offset;
                        break;
                    case Direction.Down:
                        aim += movement.Offset;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return (depth, hPosition);
        }

        public static Movement ConvertToMovement(this string value)
        {
            var values = value.Split(' ');
            return new Movement
            {
                Direction = Enum.Parse<Direction>(values[0], true), 
                Offset = int.Parse(values[1])
            };
        }
    }
}