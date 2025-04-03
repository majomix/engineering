using Algorithms.Graph.Dijkstra;
using DataStructures.PriorityQueue;
using FluentAssertions;
using NUnit.Framework;
using System.Diagnostics;
using System.Drawing;
using DataStructures.Helpers;

namespace AdventOfCode._2024;

internal static class Day16
{
    /// <summary>
    /// It's time again for the Reindeer Olympics! This year, the big event is the Reindeer Maze, where the Reindeer compete for the lowest score
    ///
    /// You and The Historians arrive to search for the Chief right as the event is about to start. It wouldn't hurt to watch a little, right?
    /// The Reindeer start on the Start Tile (marked S) facing East and need to reach the End Tile (marked E). They can move forward one tile at a time (increasing their score by 1 point),
    /// but never into a wall(#). They can also rotate clockwise or counterclockwise 90 degrees at a time (increasing their score by 1000 points).
    /// To figure out the best place to sit, you start by grabbing a map (your puzzle input) from a nearby kiosk.
    ///
    /// Analyze your map carefully. What is the lowest score a Reindeer could possibly get?
    /// </summary>
    public static int GetLowestScoreForMaze(string[] input)
    {
        var graph = input.Select(s => s.Select(r => r).ToArray()).ToArray();

        return FindShortestPath(graph).PriceOfCheapestWay;
    }

    public static (int PriceOfCheapestWay, HashSet<Point> TilesInPath) FindShortestPath(char[][] graph)
    {
        const int infinity = DijkstraResult<DirectedPoint>.Infinity;
        const int scoreForRotation = 1000;
        const int scoreForStraightMove = 1;

        var result = new DijkstraResult<DirectedPoint>();
        var priorityQueue = new CustomMinPriorityQueueByMinHeap<int, DirectedPoint>();

        InitializeDijkstra(graph, priorityQueue, result);

        while (priorityQueue.Count > 0)
        {
            var minimum = priorityQueue.ExtractMinimum();

            var neighbours = GetRotatedNeighbours(minimum)
                .Select(n => (n, scoreForRotation))
                .Append((GetStraightNeighbour(minimum), scoreForStraightMove))
                .ToList();

            foreach (var (neighbourPoint, neighbourPrice) in neighbours)
            {
                if (graph[neighbourPoint.Y][neighbourPoint.X] != '#' && result.PathWeight[minimum] != infinity)
                {
                    var newWeight = result.PathWeight[minimum] + neighbourPrice;
                    var oldWeight = result.PathWeight[neighbourPoint];

                    if (newWeight < oldWeight)
                    {
                        result.PathWeight[neighbourPoint] = newWeight;
                        result.PreviousVertex[neighbourPoint] = minimum;
                        result.PreviousVertices[neighbourPoint].Add(minimum);

                        priorityQueue.DecreasePriority(neighbourPoint, result.PathWeight[neighbourPoint]);
                    }
                    else if (newWeight == oldWeight)
                    {
                        result.PreviousVertices[neighbourPoint].Add(minimum);
                    }
                }
            }
        }

        var bestPath = result
            .PathWeight
            .Where(v => v.Key.X == graph[0].Length - 2 && v.Key.Y == 1)
            .MinBy(s => s.Value);

        var uniqueVisitedPoints = new HashSet<Point>();
        GatherPointsFromMap(bestPath.Key, result, graph, uniqueVisitedPoints);

        return (bestPath.Value, uniqueVisitedPoints);
    }

    private static void GatherPointsFromMap(DirectedPoint currentPoint, DijkstraResult<DirectedPoint> result, char[][] graph, HashSet<Point> points)
    {
        var arrows = new[] { '^', 'v', '<', '>' };
        var shouldStop = graph[currentPoint.Y][currentPoint.X] == 'S';

        points.Add(new Point(currentPoint.X, currentPoint.Y));

        if (shouldStop)
            return;

        graph[currentPoint.Y][currentPoint.X] = arrows[(int)currentPoint.Direction];

        foreach (var previous in result.PreviousVertices[currentPoint])
        {
            GatherPointsFromMap(previous, result, graph, points);
        }
    }

    private static void InitializeDijkstra(char[][] graph, CustomMinPriorityQueueByMinHeap<int, DirectedPoint> priorityQueue, DijkstraResult<DirectedPoint> result)
    {
        var infinity = DijkstraResult<DirectedPoint>.Infinity;

        // initialize distance to all other vertices to infinity and their predecessor as unknown
        for (var column = 0; column < graph.Length; column++)
        {
            for (var row = 0; row < graph[column].Length; row++)
            {
                var cell = graph[column][row];
                var point = new Point(row, column);

                if (cell is '.' or 'E' or 'S')
                {
                    for (var i = 0; i < 4; i++)
                    {
                        var directedPoint = new DirectedPoint(point, (PointDirection)i);

                        if (cell is 'S' && directedPoint.Direction == PointDirection.Right)
                        {
                            var origin = new DirectedPoint(point, PointDirection.Right);
                            priorityQueue.Insert(0, origin);
                            result.PathWeight[origin] = 0;
                        }
                        else
                        {
                            priorityQueue.Insert(DijkstraResult<Point>.Infinity, directedPoint);
                            result.PathWeight[directedPoint] = infinity;
                            result.PreviousVertex[directedPoint] = DijkstraResult<DirectedPoint>.UnknownParent;
                            result.PreviousVertices[directedPoint] = new();
                        }
                    }
                }
            }
        }
    }

    private static DirectedPoint GetStraightNeighbour(DirectedPoint currentPoint)
    {
        switch (currentPoint.Direction)
        {
            case PointDirection.Up:
                var above = new Point(currentPoint.X, currentPoint.Y - 1);
                return new DirectedPoint(above, PointDirection.Up);
            case PointDirection.Left:
                var left = new Point(currentPoint.X - 1, currentPoint.Y);
                return new DirectedPoint(left, PointDirection.Left);
            case PointDirection.Right:
                var right = new Point(currentPoint.X + 1, currentPoint.Y);
                return new DirectedPoint(right, PointDirection.Right);
            case PointDirection.Down:
                var below = new Point(currentPoint.X, currentPoint.Y + 1);
                return new DirectedPoint(below, PointDirection.Down);
            default:
                throw new Exception();
        }
    }

    private static List<DirectedPoint> GetRotatedNeighbours(DirectedPoint currentPoint)
    {
        var pointCopy = new Point(currentPoint.X, currentPoint.Y);

        switch (currentPoint.Direction)
        {
            case PointDirection.Up:
            case PointDirection.Down:
                return new List<DirectedPoint> { new(pointCopy, PointDirection.Left), new (pointCopy, PointDirection.Right) };
            case PointDirection.Left:
            case PointDirection.Right:
                return new List<DirectedPoint> { new(pointCopy, PointDirection.Up), new(pointCopy, PointDirection.Down) };
            default:
                throw new Exception();
        }
    }

    private static void PrintPath(char[][] graph)
    {
        for (var row = 0; row < graph.Length; row++)
        {
            for (var column = 0; column < graph[0].Length; column++)
            {
                Debug.Write(graph[row][column]);
            }
            Debug.WriteLine(string.Empty);
        }
    }

    /// <summary>
    /// Now that you know what the best paths look like, you can figure out the best spot to sit.
    /// Every non-wall tile (S, ., or E) is equipped with places to sit along the edges of the tile.
    /// While determining which of these tiles would be the best spot to sit depends on a whole bunch of factors.
    /// The most important factor is whether the tile is on one of the best paths through the maze. If you sit somewhere else, you'd miss all the action!
    ///
    /// So, you'll need to determine which tiles are part of any best path through the maze, including the S and E tiles.
    ///
    /// Analyze your map further.How many tiles are part of at least one of the best paths through the maze?
    /// </summary>
    public static int GetNumberOfUniqueTilesInAllShortestPaths(string[] input)
    {
        var graph = input.Select(s => s.Select(r => r).ToArray()).ToArray();
        return FindShortestPath(graph).TilesInPath.Count;
    }
}

[TestFixture]
internal class Day16Tests
{
    [Test]
    public void Day16Task1Example1()
    {
        string[] input =
        {
            "###############",
            "#.......#....E#",
            "#.#.###.#.###.#",
            "#.....#.#...#.#",
            "#.###.#####.#.#",
            "#.#.#.......#.#",
            "#.#.#####.###.#",
            "#...........#.#",
            "###.#.#####.#.#",
            "#...#.....#.#.#",
            "#.#.#.###.#.#.#",
            "#.....#...#.#.#",
            "#.###.#.#.#.#.#",
            "#S..#.....#...#",
            "###############"
        };
        Day16.GetLowestScoreForMaze(input).Should().Be(7036);
    }

    [Test]
    public void Day16Task1Example2()
    {
        string[] input =
        {
            "#################",
            "#...#...#...#..E#",
            "#.#.#.#.#.#.#.#.#",
            "#.#.#.#...#...#.#",
            "#.#.#.#.###.#.#.#",
            "#...#.#.#.....#.#",
            "#.#.#.#.#.#####.#",
            "#.#...#.#.#.....#",
            "#.#.#####.#.###.#",
            "#.#.#.......#...#",
            "#.#.###.#####.###",
            "#.#.#...#.....#.#",
            "#.#.#.#####.###.#",
            "#.#.#.........#.#",
            "#.#.#.#########.#",
            "#S#.............#",
            "#################",
        };

        Day16.GetLowestScoreForMaze(input).Should().Be(11048);
    }

    [Test]
    public void Day16Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day16.txt");
        var result = Day16.GetLowestScoreForMaze(input);
        result.Should().Be(106512);
    }

    [Test]
    public void Day16Task2Example1()
    {
        string[] input =
        {
            "###############",
            "#.......#....E#",
            "#.#.###.#.###.#",
            "#.....#.#...#.#",
            "#.###.#####.#.#",
            "#.#.#.......#.#",
            "#.#.#####.###.#",
            "#...........#.#",
            "###.#.#####.#.#",
            "#...#.....#.#.#",
            "#.#.#.###.#.#.#",
            "#.....#...#.#.#",
            "#.###.#.#.#.#.#",
            "#S..#.....#...#",
            "###############"
        };

        Day16.GetNumberOfUniqueTilesInAllShortestPaths(input).Should().Be(45);
    }

    [Test]
    public void Day16Task2Example2()
    {
        string[] input =
        {
            "#################",
            "#...#...#...#..E#",
            "#.#.#.#.#.#.#.#.#",
            "#.#.#.#...#...#.#",
            "#.#.#.#.###.#.#.#",
            "#...#.#.#.....#.#",
            "#.#.#.#.#.#####.#",
            "#.#...#.#.#.....#",
            "#.#.#####.#.###.#",
            "#.#.#.......#...#",
            "#.#.###.#####.###",
            "#.#.#...#.....#.#",
            "#.#.#.#####.###.#",
            "#.#.#.........#.#",
            "#.#.#.#########.#",
            "#S#.............#",
            "#################",
        };

        Day16.GetNumberOfUniqueTilesInAllShortestPaths(input).Should().Be(64);
    }

    [Test]
    public void Day16Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day16.txt");
        var result = Day16.GetNumberOfUniqueTilesInAllShortestPaths(input);
        result.Should().Be(563);
    }
}