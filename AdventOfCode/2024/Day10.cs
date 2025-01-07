using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace AdventOfCode._2024;

internal static class Day10
{
    /// <summary>
    /// The reindeer is holding a book titled "Lava Island Hiking Guide". However, when you open the book,
    /// you discover that most of it seems to have been scorched by lava!
    /// 
    /// As you're about to ask how you can help, the reindeer brings you a blank topographic map of the surrounding
    /// area and looks up at you excitedly. Perhaps you can help fill in the missing hiking trails?
    /// 
    /// The topographic map indicates the height at each position using a scale from 0 (lowest) to 9 (highest). 
    /// 
    /// Based on un-scorched scraps of the book, you determine that a good hiking trail is as long as possible
    /// and has an even, gradual, uphill slope.
    /// 
    /// For all practical purposes, this means that a hiking trail is any path that starts at height 0,
    /// ends at height 9, and always increases by a height of exactly 1 at each step.
    /// 
    /// Hiking trails never include diagonal steps - only up, down, left, or right (from the perspective of the map).
    /// 
    /// A trailhead is any position that starts one or more hiking trails - here, these positions will always have
    /// height 0. Assembling more fragments of pages, you establish that a trailhead's score is the number of
    /// 9-height positions reachable from that trailhead via a hiking trail. 
    /// 
    /// What is the sum of the scores of all trailheads on your topographic map?
    /// </summary>
    public static int GetSumOfScoresOfAllTrailheads(string[] input)
    {
        var sumOfScoresOfAllTrailheads = 0;
        var trailheads = FindTrailheads(input);

        foreach (var trailhead in trailheads)
        {
            sumOfScoresOfAllTrailheads += Recurse(input, trailhead, new HashSet<Point>(), true);
        }

        return sumOfScoresOfAllTrailheads;
    }

    private static List<Point> FindTrailheads(string[] input)
    {
        var trailheads = new List<Point>();

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                if (input[y][x] == '0')
                {
                    trailheads.Add(new Point(x, y));
                }
            }
        }

        return trailheads;
    }

    private static int Recurse(string[] input, Point currentPoint, HashSet<Point> reachedTargets, bool findOnlyUnique)
    {
        var options = new List<Point>
        {
            new Point(currentPoint.X, currentPoint.Y - 1), // up
            new Point(currentPoint.X, currentPoint.Y + 1), // down
            new Point(currentPoint.X - 1, currentPoint.Y), // left
            new Point(currentPoint.X + 1, currentPoint.Y) // right
        };

        var currentValue = input[currentPoint.Y][currentPoint.X];
        var nextValue = currentValue + 1;

        if (currentValue == '9')
        {
            if (!findOnlyUnique)
            {
                return 1;
            }
            else if (!reachedTargets.Contains(currentPoint))
            {
                reachedTargets.Add(currentPoint);
                return 1;
            }
        }

        var paths = 0;

        foreach (var move in options)
        {
            if (IsInsideBounds(input, move) && input[move.Y][move.X] == nextValue)
            {
                paths += Recurse(input, move, reachedTargets, findOnlyUnique);
            }
        }

        return paths;
    }

    private static bool IsInsideBounds(string[] input, Point point)
    {
        if (point.X < 0 || point.Y < 0)
            return false;

        if (point.Y >= input.Length)
            return false;

        if (point.X >= input[0].Length)
            return false;

        return true;
    }

    /// <summary>
    /// A second way to measure a trailhead is called its rating. A trailhead's rating is the number of distinct
    /// hiking trails which begin at that trailhead.
    /// 
    /// What is the sum of the ratings of all trailheads?
    /// </summary>
    public static int GetSumOfRatingsOfAllTrailheads(string[] input)
    {
        var sumOfRatingsOfAllTrailheads = 0;
        var trailheads = FindTrailheads(input);

        foreach (var trailhead in trailheads)
        {
            sumOfRatingsOfAllTrailheads += Recurse(input, trailhead, new HashSet<Point>(), false);
        }

        return sumOfRatingsOfAllTrailheads;
    }
}

[TestFixture]
internal class Day10Tests
{
    [Test]
    public void Day10Task1Example()
    {
        string[] input =
        {
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        };

        Day10.GetSumOfScoresOfAllTrailheads(input).Should().Be(36);
    }

    [Test]
    public void Day10Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day10.txt");
        var result = Day10.GetSumOfScoresOfAllTrailheads(input);
        result.Should().Be(510);
    }

    [Test]
    public void Day10Task2Example()
    {
        string[] input =
        {
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        };

        Day10.GetSumOfRatingsOfAllTrailheads(input).Should().Be(81);
    }

    [Test]
    public void Day10Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day10.txt");
        var result = Day10.GetSumOfRatingsOfAllTrailheads(input);
        result.Should().Be(1058);
    }
}