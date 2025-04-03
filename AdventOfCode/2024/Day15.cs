using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day15
{
    /// <summary>
    /// The lanternfish already have a map of the warehouse and a list of movements the robot will attempt to make. The problem is that the movements will sometimes fail
    /// as boxes are shifted around, making the actual movements of the robot difficult to predict.
    ///
    /// As the robot (@) attempts to move, if there are any boxes (O) in the way, the robot will also attempt to push those boxes.
    /// However, if this action would cause the robot or a box to move into a wall (#), nothing moves instead, including the robot.
    /// The initial positions of these are shown on the map at the top of the document the lanternfish gave you.
    ///
    /// The rest of the document describes the moves that the robot will attempt to make, in order.
    ///
    /// The lanternfish use their own custom Goods Positioning System (GPS for short) to track the locations of the boxes. The GPS coordinate of a box is equal
    /// to 100 times its distance from the top edge of the map plus its distance from the left edge of the map.
    /// This process does not stop at wall tiles; measure all the way to the edges of the map.
    ///
    /// The lanternfish would like to know the sum of all boxes' GPS coordinates after the robot finishes moving.
    /// Predict the motion of the robot and boxes in the warehouse.
    /// 
    /// After the robot is finished moving, what is the sum of all boxes' GPS coordinates?
    /// </summary>
    public static int GetSumOfAllBoxesGps(string[] input)
    {
        var sokoban = new LanternfishSokoban(input);
        sokoban.Play();
        var result = sokoban.GetSumOfGpsCoordinates();

        return result;
    }

    /// <summary>
    ///
    /// </summary>
    public static int Task2(string[] input)
    {
        var sokoban = new ExpandedLanternfishSokoban(input);
    }
}

public class ExpandedLanternfishSokoban
{

}

public class LanternfishSokoban
{
    private readonly Queue<char> _playerMoves = new();
    private readonly HashSet<Point> _walls = new();
    private readonly HashSet<Point> _boxes = new();

    private Point _player;

    public LanternfishSokoban(string[] input)
    {
        var parsingLevel = true;

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            if (parsingLevel)
            {
                if (string.Empty.Equals(line))
                {
                    parsingLevel = false;
                }
                else
                {
                    for (var j = 0; j < line.Length; j++)
                    {
                        var cell = line[j];
                        var cellLocation = new Point(j, i);
                        switch (cell)
                        {
                            case '#':
                                _walls.Add(cellLocation);
                                break;
                            case 'O':
                                _boxes.Add(cellLocation);
                                break;
                            case '@':
                                _player = cellLocation;
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (var ch in line)
                {
                    _playerMoves.Enqueue(ch);
                }
            }
        }
    }

    public void Play()
    {
        foreach (var move in _playerMoves)
        {
            _player = TryMove(_player, move);
        }
    }

    public int GetSumOfGpsCoordinates()
    {
        var sum = 0;

        foreach (var box in _boxes)
        {
            sum += box.Y * 100 + box.X;
        }

        return sum;
    }

    private Point TryMove(Point currentPosition, char move)
    {
        var targetPosition = GetTargetPosition(currentPosition, move);

        if (_walls.Contains(targetPosition))
            return currentPosition;

        if (!_boxes.Contains(targetPosition))
            return targetPosition;

        var targetPositionOfMovedBox = TryMove(targetPosition, move);
        
        if (targetPositionOfMovedBox != targetPosition)
        {
            _boxes.Remove(targetPosition);
            _boxes.Add(targetPositionOfMovedBox);

            return targetPosition;
        }

        return currentPosition;
    }
    
    protected Point GetTargetPosition(Point currentPosition, char move)
    {
        switch (move)
        {
            case '<':
                return currentPosition with { X = currentPosition.X - 1 };
            case '^':
                return currentPosition with { Y = currentPosition.Y - 1 };
            case '>':
                return currentPosition with { X = currentPosition.X + 1 };
            case 'v':
                return currentPosition with { Y = currentPosition.Y + 1 };
        }

        throw new InvalidDataException("No such move known.");
    }
}

[TestFixture]
internal class Day15Tests
{
    [Test]
    public void Day15Task1Example1()
    {
        string[] input =
        {
            "########",
            "#..O.O.#",
            "##@.O..#",
            "#...O..#",
            "#.#.O..#",
            "#...O..#",
            "#......#",
            "########",
            "",
            "<^^>>>vv<v>>v<<"
        };

        Day15.GetSumOfAllBoxesGps(input).Should().Be(2028);
    }

    [Test]
    public void Day15Task1Example2()
    {
        string[] input =
        {
            "##########",
            "#..O..O.O#",
            "#......O.#",
            "#.OO..O.O#",
            "#..O@..O.#",
            "#O#..O...#",
            "#O..O..O.#",
            "#.OO.O.OO#",
            "#....O...#",
            "##########",
            "",
            "<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^",
            "vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v",
            "><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<",
            "<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^",
            "^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><",
            "^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^",
            ">^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^",
            "<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>",
            "^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>",
            "v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^"
        };

        Day15.GetSumOfAllBoxesGps(input).Should().Be(10092);
    }

    [Test]
    public void Day15Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day15.txt");
        var result = Day15.GetSumOfAllBoxesGps(input);
        result.Should().Be(1517819);
    }

    [Test]
    public void Day15Task2Example()
    {
        string[] input =
        {

        };

        Day15.Task2(input).Should().Be(0);
    }

    [Test]
    public void Day15Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day15.txt");
        var result = Day15.Task2(input);
        result.Should().Be(0);
    }
}