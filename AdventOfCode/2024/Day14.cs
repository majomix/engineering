using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace AdventOfCode._2024;

internal static class Day14
{
    /// <summary>
    /// The area outside the bathroom is swarming with robots! To get The Historian safely to the bathroom, you'll need a way to predict where the robots will be in the future.
    /// Fortunately, they all seem to be moving on the tile floor in predictable straight lines. You make a list of all of the robots' current positions (p)
    /// and velocities (v), one robot per line.
    ///
    /// Each robot's position is given as p=x,y where x represents the number of tiles the robot is from the left wall and y represents the number of tiles from the top wall
    /// (when viewed from above). So, a position of p=0,0 means the robot is all the way in the top-left corner.
    ///
    /// Each robot's velocity is given as v=x,y where x and y are given in tiles per second. Positive x means the robot is moving to the right, and positive y means the robot
    /// is moving down. So, a velocity of v=1,-2 means that each second, the robot moves 1 tile to the right and 2 tiles up.
    ///
    /// The robots outside the actual bathroom are in a space which is 101 tiles wide and 103 tiles tall when viewed from above.
    /// The robots are good at navigating over/under each other (due to a combination of springs, extendable legs, and quadcopters), so they can share the same tile
    /// and don't interact with each other.
    ///
    /// These robots have a unique feature for maximum bathroom security: they can teleport. When a robot would run into an edge of the space they're in, they instead teleport
    /// to the other side, effectively wrapping around the edges.
    ///
    /// To determine the safest area, count the number of robots in each quadrant after 100 seconds. Robots that are exactly in the middle (horizontally or vertically)
    /// don't count as being in any quadrant.
    /// </summary>
    public static int GetSafetyFactor(string[] input, Point gridSize)
    {
        var secondsToSimulate = 100;

        var robots = ParseInput(input);
        var bathroom = new Bathroom();

        foreach (var robot in robots)
        {
            var targetPosition = robot.SimulateMovement(gridSize, secondsToSimulate);
            bathroom.AddRobot(targetPosition);
        }

        return bathroom.GetSafetyFactor(gridSize);
    }

    private static List<BathroomRobot> ParseInput(string[] input)
    {
        var result = new List<BathroomRobot>();

        foreach (var line in input)
        {
            var split = line.Split(new[] { "p=", " v=", "," }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 4)
            {
                var position = new Point(int.Parse(split[0]), int.Parse(split[1]));
                var velocity = new Point(int.Parse(split[2]), int.Parse(split[3]));
                var robot = new BathroomRobot(position, velocity);

                result.Add(robot);
            }
        }

        return result;
    }

    /// <summary>
    /// During the bathroom break, someone notices that these robots seem awfully similar to ones built and used at the North Pole.
    /// If they're the same type of robots, they should have a hard-coded Easter egg: very rarely, most of the robots should arrange themselves into a picture
    /// of a Christmas tree.
    ///
    /// What is the fewest number of seconds that must elapse for the robots to display the Easter egg?
    /// </summary>
    public static int GetNumberOfSecondsForChristmasTreeArrangement(string[] input, Point gridSize)
    {
        var robots = ParseInput(input);
        var simulationAnalysisResult = 7623;

        var bathroom = new Bathroom();

        foreach (var robot in robots)
        {
            var targetPosition = robot.SimulateMovement(gridSize, simulationAnalysisResult);
            bathroom.AddRobot(targetPosition);
        }

        bathroom.Print(gridSize, simulationAnalysisResult);

        return simulationAnalysisResult;
    }
}

public class BathroomRobot
{
    private readonly Point _position;
    private readonly Point _velocity;

    public BathroomRobot(Point position, Point velocity)
    {
        _position = position;
        _velocity = velocity;
    }

    public Point SimulateMovement(Point gridSize, int secondsToSimulate)
    {
        var targetX = (gridSize.X + ((_position.X + _velocity.X * secondsToSimulate) % gridSize.X)) % gridSize.X;
        var targetY = (gridSize.Y + ((_position.Y + _velocity.Y * secondsToSimulate) % gridSize.Y)) % gridSize.Y;

        return new Point(targetX, targetY);
    }
}

public class Bathroom
{
    private class Quadrant
    {
        public Point LeftTop { get; }
        public Point RightBottom { get; }

        public static List<Quadrant> CreateQuadrants(Point gridSize)
        {
            var endOfLeftQuadrantX = gridSize.X / 2;
            var startOfRightQuadrantX = endOfLeftQuadrantX + 1;
            var endOfUpperQuadrantY = gridSize.Y / 2;
            var startOfLowerQuadrantY = endOfUpperQuadrantY + 1;

            var quadrants = new List<Quadrant>
            {
                new(new Point(0, 0), new Point(endOfLeftQuadrantX, endOfUpperQuadrantY)),
                new(new Point(startOfRightQuadrantX, 0), new Point(gridSize.X, endOfUpperQuadrantY)),
                new(new Point(0, startOfLowerQuadrantY), new Point(endOfLeftQuadrantX, gridSize.Y)),
                new(new Point(startOfRightQuadrantX, startOfLowerQuadrantY), new Point(gridSize.X, gridSize.Y))
            };

            return quadrants;
        }

        private Quadrant(Point leftTop, Point rightBottom)
        {
            LeftTop = leftTop;
            RightBottom = rightBottom;
        }
    }

    private readonly Dictionary<Point, int> _robots = new();

    public void AddRobot(Point position)
    {
        if (!_robots.TryAdd(position, 1))
        {
            _robots[position]++;
        }
    }

    public int GetSafetyFactor(Point gridSize)
    {
        var quadrants = Quadrant.CreateQuadrants(gridSize);

        var safetyFactor = 1;

        foreach (var quadrant in quadrants)
        {
            var robotsInQuadrant = 0;

            for (var y = quadrant.LeftTop.Y; y < quadrant.RightBottom.Y; y++)
            {
                for (var x = quadrant.LeftTop.X; x < quadrant.RightBottom.X; x++)
                {
                    if (_robots.TryGetValue(new Point(x, y), out var robotsInCell))
                    {
                        robotsInQuadrant += robotsInCell;
                    }
                }
            }

            if (robotsInQuadrant != 0)
            {
                safetyFactor *= robotsInQuadrant;
            }
        }

        return safetyFactor;
    }

    public void Print(Point gridSize, int steps)
    {
        Debug.WriteLine($"STEPS: {steps}");

        for (var y = 0; y < gridSize.Y; y++)
        {
            for (var x = 0; x < gridSize.X; x++)
            {
                if (_robots.TryGetValue(new Point(x, y), out var value))
                {
                    Debug.Write(value);
                }
                else
                {
                    Debug.Write(".");
                }
            }
            Debug.WriteLine(string.Empty);
        }
    }
}

[TestFixture]
internal class Day14Tests
{
    [Test]
    public void Day14Task1Example()
    {
        string[] input =
        {
            "p=0,4 v=3,-3",
            "p=6,3 v=-1,-3",
            "p=10,3 v=-1,2",
            "p=2,0 v=2,-1",
            "p=0,0 v=1,3",
            "p=3,0 v=-2,-2",
            "p=7,6 v=-1,-3",
            "p=3,0 v=-1,-2",
            "p=9,3 v=2,3",
            "p=7,3 v=-1,2",
            "p=2,4 v=2,-3",
            "p=9,5 v=-3,-3"
        };

        Day14.GetSafetyFactor(input, new Point(11, 7)).Should().Be(12);
    }

    [Test]
    public void Day14Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day14.txt");
        var result = Day14.GetSafetyFactor(input, new Point(101, 103));
        result.Should().Be(222208000);
    }

    [Test]
    public void Day14Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day14.txt");
        var result = Day14.GetNumberOfSecondsForChristmasTreeArrangement(input, new Point(101, 103));
        result.Should().Be(7623);
    }
}