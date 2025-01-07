using FluentAssertions;
using NUnit.Framework;
using System.Diagnostics;
using System.Drawing;

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
    /// TODO: debug, should work fine
    public static int GetNumberOfObstaclesCausingLoop(string[] input)
    {
        var distinctVisitedElements = 1;

        var obstaclesMap = new HashSet<Point>();
        var currentX = 0;
        var currentY = 0;
        var inputChars = ParseInputAndFindStartingPoint(input, ref currentX, ref currentY);

        Debug.WriteLine($"Loop detection for starting point: {currentX}, {currentY}");
        var isInsideBounds = true;
        var movesInCurrentDirection = 0;
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

            if (canContinueInSameDirection && movesInCurrentDirection > 0)
            {
                var mapWithObstacle = CopyMapWithObstacle(inputChars, targetX, targetY);
                Debug.WriteLine($"Placing obstacle at {targetX} {targetY}.\nLoop detection at distinct #{distinctVisitedElements}: {currentX}, {currentY}, {inputChars[currentY][currentX]}");

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
                movesInCurrentDirection++;
            }
            else
            {
                movesInCurrentDirection = 0;
            }

            if (isNextMoveLandingOnUnvisitedPosition)
            {
                distinctVisitedElements++;
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
        var originalX = currentX;
        var originalY = currentY;
        var originalDirection = map[currentY][currentX];
        var hmap = new HashSet<(int, int, char)>();

        var steps = 0;
        var isInsideBounds = true;
        while (isInsideBounds)
        {
            var currentPlayerDirection = map[currentY][currentX];

            //Debug.WriteLine($"{currentX}, {currentY}, {currentPlayerDirection}");

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

            if (steps > 0 && hmap.Contains((currentX, currentY, currentPlayerDirection)))
            {
                Debug.WriteLine($"Loop fiund at {currentX} {currentY} in direction {currentPlayerDirection}");
                return true;
            }

            var canContinueInSameDirection = CanContinueInSameDirection(map, targetX, targetY);

            Move(map, currentX, currentY, targetX, targetY, possiblePlayerRotation);

            if (canContinueInSameDirection)
            {
                hmap.Add((currentX, currentY, currentPlayerDirection));

                currentX = targetX;
                currentY = targetY;
            }
            steps++;

        }

        Debug.WriteLine("Loop not found.");
        return false;
    }

    private static void Print(List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[y].Count; x++)
            {
                Debug.Write($"{map[y][x]}");
            }
            Debug.WriteLine(string.Empty);
        }
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

    //[Test]
    public void Day6Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day6.txt");
        var result = Day6.GetNumberOfObstaclesCausingLoop(input);
        result.Should().Be(0);
    }
}