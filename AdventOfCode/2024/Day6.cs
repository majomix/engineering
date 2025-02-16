using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using System.Text;

namespace AdventOfCode._2024;

internal static class Day6
{
    /// <summary>
    /// A guard is patrolling part of the lab. Maybe you can work out where the guard will go ahead of time
    /// so that The Historians can search safely? You start by making a map of the situation.
    /// 
    /// The map shows the current position of the guard with ^
    /// (to indicate the guard is currently facing up from the perspective of the map).
    /// Any obstructions - crates, desks, alchemical reactors, etc. - are shown as #.
    /// 
    /// Lab guards in 1518 follow a very strict patrol protocol which involves repeatedly following these steps:
    /// - If there is something directly in front of you, turn right 90 degrees.
    /// - Otherwise, take a step forward.
    /// 
    /// Predict the path of the guard.
    /// How many distinct positions will the guard visit before leaving the mapped area?
    /// </summary>
    public static int PredictNumberOfDistinctPositions(string[] input)
    {
        var distinctVisitedElements = 1;

        var currentX = 0;
        var currentY = 0;
        var inputChars = ParseInputAndFindStartingPoint(input, ref currentX, ref currentY);

        var isInsideBounds = true;
        while (isInsideBounds)
        {
            var currentPlayerDirection = inputChars[currentY][currentX];
            var possiblePlayerRotation = currentPlayerDirection;
            var targetX = currentX;
            var targetY = currentY;
            DetermineNextMove(
                currentX,
                currentY,
                currentPlayerDirection,
                ref possiblePlayerRotation,
                ref targetX,
                ref targetY);

            isInsideBounds = IsInsideBounds(inputChars, targetX, targetY);
            if (!isInsideBounds)
                continue;

            var canContinueInSameDirection = CanContinueInSameDirection(inputChars, targetX, targetY);
            var isNextMoveLandingOnUnvisitedPosition = IsNextMoveLandingOnUnvisitedPosition(inputChars, targetX, targetY);

            Move(inputChars, currentX, currentY, targetX, targetY, possiblePlayerRotation);

            if (canContinueInSameDirection)
            {
                currentX = targetX;
                currentY = targetY;
            }

            if (isNextMoveLandingOnUnvisitedPosition)
            {
                distinctVisitedElements++;
            }
        }

        return distinctVisitedElements;
    }

    private static List<List<char>> ParseInputAndFindStartingPoint(string[] input, ref int currentX, ref int currentY)
    {
        var inputChars = new List<List<char>>();

        for (var y = 0; y < input.Length; y++)
        {
            var rowChars = new List<char>();
            for (var x = 0; x < input[y].Length; x++)
            {
                rowChars.Add(input[y][x]);

                if (input[y][x] == '^')
                {
                    currentX = x;
                    currentY = y;
                }
            }

            inputChars.Add(rowChars);
        }

        return inputChars;
    }

    private static bool IsInsideBounds(List<List<char>> input, int x, int y)
    {
        if (x < 0 || y < 0)
            return false;

        if (y >= input.Count)
            return false;

        if (x >= input[0].Count)
            return false;

        return true;
    }

    private static bool CanContinueInSameDirection(
        List<List<char>> input,
        int targetX,
        int targetY)
    {
        var nextStep = input[targetY][targetX];

        return nextStep != '#';
    }

    private static bool IsNextMoveLandingOnUnvisitedPosition(
        List<List<char>> input,
        int targetX,
        int targetY)
    {
        var nextStep = input[targetY][targetX];

        return nextStep == '.';
    }

    private static void Move(
        List<List<char>> input,
        int currentX,
        int currentY,
        int targetX,
        int targetY,
        char rotation)
    {
        var nextStep = input[targetY][targetX];
        switch (nextStep)
        {
            case '#':
                input[currentY][currentX] = rotation;
                break;
            case '.':
                input[targetY][targetX] = input[currentY][currentX];
                input[currentY][currentX] = 'X';
                break;
            case 'X':
                input[targetY][targetX] = input[currentY][currentX];
                input[currentY][currentX] = 'X';
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// Returning after what seems like only a few seconds to The Historians, they explain that the guard's patrol
    /// area is simply too large for them to safely search the lab without getting caught.
    /// 
    /// Fortunately, they are pretty sure that adding a single new obstruction won't cause a time paradox.
    /// They'd like to place the new obstruction in such a way that the guard will get stuck in a loop,
    /// making the rest of the lab safe to search.
    /// 
    /// To have the lowest chance of creating a time paradox, The Historians would like to know all of the
    /// possible positions for such an obstruction.
    /// The new obstruction can't be placed at the guard's starting position - the guard is there right now
    /// and would notice.
    /// 
    /// How many different positions could you choose for this obstruction?
    /// </summary>
    public static int GetNumberOfObstaclesCausingLoop(string[] input)
    {
        var obstaclesMap = new HashSet<Point>();
        var visitedMap = new HashSet<Point>();
        var currentX = 0;
        var currentY = 0;
        var inputChars = ParseInputAndFindStartingPoint(input, ref currentX, ref currentY);

        var isInsideBounds = true;
        while (isInsideBounds)
        {
            //var currentMapState = PrintIntoString(inputChars);

            var currentPlayerDirection = inputChars[currentY][currentX];
            var possiblePlayerRotation = currentPlayerDirection;
            visitedMap.Add(new Point(currentX, currentY));
            var targetX = currentX;
            var targetY = currentY;
            DetermineNextMove(
                currentX,
                currentY,
                currentPlayerDirection,
                ref possiblePlayerRotation,
                ref targetX,
                ref targetY);

            isInsideBounds = IsInsideBounds(inputChars, targetX, targetY);
            if (!isInsideBounds)
                continue;

            var canContinueInSameDirection = CanContinueInSameDirection(inputChars, targetX, targetY);

            if (canContinueInSameDirection && !visitedMap.Contains(new Point(targetX, targetY)))
            {
                var mapWithObstacle = CopyMapWithObstacle(inputChars, targetX, targetY);

                if (ContainsLoop(mapWithObstacle, currentX, currentY))
                {
                    obstaclesMap.Add(new Point(targetX, targetY));
                }
            }

            Move(inputChars, currentX, currentY, targetX, targetY, possiblePlayerRotation);

            if (canContinueInSameDirection)
            {
                currentX = targetX;
                currentY = targetY;
            }
        }

        return obstaclesMap.Count;
    }

    private static void DetermineNextMove(int currentX, int currentY, char currentPlayerDirection, ref char possiblePlayerRotation, ref int targetX, ref int targetY)
    {
        switch (currentPlayerDirection)
        {
            case '^':
                targetY = currentY - 1;
                possiblePlayerRotation = '>';
                break;
            case '>':
                targetX = currentX + 1;
                possiblePlayerRotation = 'v';
                break;
            case 'v':
                targetY = currentY + 1;
                possiblePlayerRotation = '<';
                break;
            case '<':
                targetX = currentX - 1;
                possiblePlayerRotation = '^';
                break;
        }
    }

    private static List<List<char>> CopyMapWithObstacle(List<List<char>> inputChars, int targetX, int targetY)
    {
        var copy = new List<List<char>>();

        for (var y = 0; y < inputChars.Count; y++)
        {
            var currentRow = new List<char>();

            for (var x = 0; x < inputChars[y].Count; x++)
            {
                if (x == targetX && y == targetY)
                {
                    currentRow.Add('#');
                }
                else
                {
                    currentRow.Add(inputChars[y][x]);
                }
            }

            copy.Add(currentRow);
        }

        return copy;
    }

    private static bool ContainsLoop(List<List<char>> map, int currentX, int currentY)
    {
        var movesMap = new HashSet<(int, int, char)>();

        var steps = 0;
        var isInsideBounds = true;
        while (isInsideBounds)
        {
            //var currentMapState = PrintIntoString(map);

            var currentPlayerDirection = map[currentY][currentX];

            var possiblePlayerRotation = currentPlayerDirection;
            var targetX = currentX;
            var targetY = currentY;
            DetermineNextMove(
                currentX,
                currentY,
                currentPlayerDirection,
                ref possiblePlayerRotation,
                ref targetX,
                ref targetY);

            isInsideBounds = IsInsideBounds(map, targetX, targetY);
            if (!isInsideBounds)
                continue;

            if (steps > 0 && movesMap.Contains((currentX, currentY, currentPlayerDirection)))
            {
                return true;
            }

            var canContinueInSameDirection = CanContinueInSameDirection(map, targetX, targetY);

            Move(map, currentX, currentY, targetX, targetY, possiblePlayerRotation);

            if (canContinueInSameDirection)
            {
                movesMap.Add((currentX, currentY, currentPlayerDirection));

                currentX = targetX;
                currentY = targetY;
            }
            steps++;
        }

        return false;
    }

    private static string[] PrintIntoString(List<List<char>> map)
    {
        var result = new List<string>();

        for (var y = 0; y < map.Count; y++)
        {
            var sb = new StringBuilder();
            for (var x = 0; x < map[y].Count; x++)
            {
                sb.Append($"{map[y][x]}");
            }
            result.Add(sb.ToString());
        }

        return result.ToArray();
    }
}

[TestFixture]
internal class Day6Tests
{
    [Test]
    public void Day6Task1Example()
    {
        string[] input =
        {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        };

        Day6.PredictNumberOfDistinctPositions(input).Should().Be(41);
    }

    [Test]
    public void Day6Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day6.txt");
        var result = Day6.PredictNumberOfDistinctPositions(input);
        result.Should().Be(4973);
    }

    [Test]
    public void Day6Task2Example()
    {
        string[] input =
        {
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#...",
        };

        Day6.GetNumberOfObstaclesCausingLoop(input).Should().Be(6);
    }

    [Test]
    public void Day6Task2Example2()
    {
        string[] input =
        {
            "#.....",
            ".....#",
            "^.#..#",
            "....#."
        };

        Day6.GetNumberOfObstaclesCausingLoop(input).Should().Be(1);
    }


    [Test]
    public void Day6Task2Example3()
    {
        string[] input =
        {
            "#.....",
            ".....#",
            "^.#..#",
            "....#."
        };

        Day6.GetNumberOfObstaclesCausingLoop(input).Should().Be(1);
    }

    [Test]
    public void Day6Task2Example4()
    {
        string[] input =
        {
            "...........#.....#......",
            "...................#....",
            "...#.....##.............",
            "......................#.",
            "..................#.....",
            "..#.....................",
            "....................#...",
            "........................",
            ".#........^.............",
            "..........#..........#..",
            "..#.....#..........#....",
            "........#.....#..#......"
        };

        Day6.GetNumberOfObstaclesCausingLoop(input).Should().Be(19);
    }

    [Test]
    public void Day6Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day6.txt");
        var result = Day6.GetNumberOfObstaclesCausingLoop(input);
        result.Should().Be(1482);
    }
}