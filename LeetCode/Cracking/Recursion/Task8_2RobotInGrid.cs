using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// A robot is sitting on the upper left corner of grid with r rows and c columns. The robot can only move in two directions,
/// right and down, but certain cells are "off limits" such that the robot cannot step on them.
/// 
/// Design an algorithm to find a path for the robot from the top left to the bottom right.
///
/// Solution:
/// Brute-force with runtime O(2^(rows+columns)) since each path has rows+columns steps.
/// 
/// Memoization with runtime O(rows*columns):
/// - Example of repeating sub-problem happens after initial move down and right, and right and down.
/// - Because we do not want all solutions, rather just the first solution, it is enough to keep memo of visited dead-way points.
/// - Final solution makes sure each point is only visited once.
/// </summary>
internal class Task8_2RobotInGrid
{
    public List<Point> FindPath(char[][] grid)
    {
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            return new List<Point>();

        var memo = new HashSet<Point>();

        var res = FindPathRecursively(
            grid,
            new Point(0,0),
            new Point(grid.Length - 1, grid[0].Length - 1),
            new List<Point>(),
            memo);
        return res;
    }

    private List<Point> FindPathRecursively(
        char[][] grid,
        Point currentPoint,
        Point targetPoint,
        List<Point> currentPath,
        HashSet<Point> memo)
    {
        if (memo.Contains(currentPoint))
            return new List<Point>();

        var pathWithCurrentPoint = currentPath.ToList();
        pathWithCurrentPoint.Add(currentPoint);

        if (currentPoint == targetPoint)
            return pathWithCurrentPoint;

        if (grid.Length <= currentPoint.Y || grid[0].Length <= currentPoint.X)
            return new List<Point>();

        if (grid[currentPoint.Y][currentPoint.X] == 'x')
            return new List<Point>();

        var downPath = FindPathRecursively(
            grid,
            new Point(currentPoint.X, currentPoint.Y + 1),
            targetPoint,
            pathWithCurrentPoint,
            memo);
        if (downPath.Count != 0)
            return downPath;

        var rightPath = FindPathRecursively(
            grid,
            new Point(currentPoint.X + 1, currentPoint.Y),
            targetPoint,
            pathWithCurrentPoint,
            memo);
        if (rightPath.Count != 0)
            return rightPath;

        memo.Add(currentPoint);

        return new List<Point>();
    }
}

[TestFixture]
public class Task8_2RobotInGridTests
{
    private static object[] testCases =
    {
        new object[]
        {
            new char[][]
            {
                new char[] { '.', '.', '.', '.' },
                new char[] { 'x', '.', '.', '.' },
                new char[] { '.', 'x', '.', '.' },
                new char[] { '.', '.', 'x', '.' },
            },
            new List<Point>
            {
                new Point(0,0),
                new Point(1,0),
                new Point(1,1),
                new Point(2,1),
                new Point(2,2),
                new Point(3,2),
                new Point(3,3)
            }
        },
        new object[]
        {
            new char[][]
            {
                new char[] { '.' },
            },
            new List<Point>
            {
                new Point(0,0),
            }
        },
        new object[]
        {
            new char[][]
            {
                new char[] { '.', '.', '.', '.' },
                new char[] { '.', '.', '.', '.' },
                new char[] { '.', '.', '.', '.' },
                new char[] { '.', '.', '.', '.' },
            },
            new List<Point>
            {
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),
                new Point(0,3),
                new Point(1,3),
                new Point(2,3),
                new Point(3,3)
            }
        },
        new object[] // memo reduced recursions from 59 to 27
        {
            new char[][]
            {
                new char[] { '.', '.', '.', '.' },
                new char[] { '.', '.', '.', '.' },
                new char[] { '.', '.', '.', 'x' },
                new char[] { '.', '.', 'x', '.' },
            },
            new List<Point>()
        },
    };

    [TestCaseSource(nameof(testCases))]
    public void FindPathTests(char[][] grid, List<Point> expectedResult)
    {
        // arrange
        var sut = new Task8_2RobotInGrid();

        // act
        var result = sut.FindPath(grid);

        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}