using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace AdventOfCode._2024;

internal static class Day8
{
    /// <summary>
    /// Scanning across the city, you find that there are many antennas. Each antenna is tuned to a specific
    /// frequency indicated by a single lowercase letter, uppercase letter or digit.
    /// You create a map of these antennas.
    /// 
    /// The signal only applies its effect at specific antinodes based on the resonant frequencies of the antennas.
    /// In particular, an antinode occurs at any point that is perfectly in line with two antennas of the same
    /// frequency - but only when one of the antennas is twice as far away as the other.
    /// This means that for any pair of antennas with the same frequency, there are two antinodes,
    /// one on either side of them.
    /// 
    /// Calculate the impact of the signal.
    /// How many unique locations within the bounds of the map contain an antinode?
    /// </summary>
    public static int GetNumberOfLocationsContainingAntinode(string[] input)
    {
        var locationsContainingAntinode = new HashSet<Point>();

        var antennas = CreateMapOfAntennas(input);

        foreach (var antennaLocations in antennas.Values)
        {
            for (var i = 0; i < antennaLocations.Count; i++)
            {
                for (var y = i + 1; y < antennaLocations.Count; y++)
                {
                    var xDelta = antennaLocations[i].X - antennaLocations[y].X;
                    var yDelta = antennaLocations[i].Y - antennaLocations[y].Y;

                    var firstAntinodeX = antennaLocations[i].X + xDelta;
                    var firstAntinodeY = antennaLocations[i].Y + yDelta;

                    if (IsInsideBounds(firstAntinodeX, input[0].Length, firstAntinodeY, input.Length))
                    {
                        locationsContainingAntinode.Add(new Point(firstAntinodeX, firstAntinodeY));
                    }

                    var secondAntinodeX = antennaLocations[y].X - xDelta;
                    var secondAntinodeY = antennaLocations[y].Y - yDelta;

                    if (IsInsideBounds(secondAntinodeX, input[0].Length, secondAntinodeY, input.Length))
                    {
                        locationsContainingAntinode.Add(new Point(secondAntinodeX, secondAntinodeY));
                    }
                }
            }
        }

        return locationsContainingAntinode.Count;
    }

    private static bool IsInsideBounds(int x, int xLimit, int y, int yLimit)
    {
        return x >= 0 && x < xLimit && y >= 0 && y < yLimit;
    }

    private static Dictionary<char, List<Point>> CreateMapOfAntennas(string[] input)
    {
        var result = new Dictionary<char, List<Point>>();

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] != '.')
                {
                    var antennaId = input[y][x];
                    result.TryAdd(antennaId, new List<Point>());
                    result[antennaId].Add(new Point(x, y));
                }
            }
        }

        return result;
    }

    /// <summary>
    /// One of The Historians asks if you took the effects of resonant harmonics into your calculations.
    /// 
    /// After updating your model, it turns out that an antinode occurs at any grid position exactly in line
    /// with at least two antennas of the same frequency, regardless of distance.
    /// 
    /// Calculate the impact of the signal using this updated model. How many unique locations within the bounds
    /// of the map contain an antinode?
    /// </summary>
    public static int GetNumberOfLocationsContainingAntinodeWithUpdatedModel(string[] input)
    {
        var locationsContainingAntinode = new HashSet<Point>();

        var antennas = CreateMapOfAntennas(input);

        foreach (var antennaLocations in antennas.Values)
        {
            for (var i = 0; i < antennaLocations.Count; i++)
            {
                for (var y = i + 1; y < antennaLocations.Count; y++)
                {
                    var xDelta = antennaLocations[i].X - antennaLocations[y].X;
                    var yDelta = antennaLocations[i].Y - antennaLocations[y].Y;

                    var antinodeX = antennaLocations[y].X;
                    var antinodeY = antennaLocations[y].Y;

                    var isUpperDiagonalInsideBounds = true;
                    while (isUpperDiagonalInsideBounds)
                    {
                        antinodeX += xDelta;
                        antinodeY += yDelta;

                        isUpperDiagonalInsideBounds = IsInsideBounds(antinodeX, input[0].Length, antinodeY, input.Length);

                        if (isUpperDiagonalInsideBounds)
                        {
                            locationsContainingAntinode.Add(new Point(antinodeX, antinodeY));
                        }
                    }

                    antinodeX = antennaLocations[i].X;
                    antinodeY = antennaLocations[i].Y;

                    var isLowerDiagonalInsideBounds = true;
                    while (isLowerDiagonalInsideBounds)
                    {
                        antinodeX -= xDelta;
                        antinodeY -= yDelta;

                        isLowerDiagonalInsideBounds = IsInsideBounds(antinodeX, input[0].Length, antinodeY, input.Length);

                        if (isLowerDiagonalInsideBounds)
                        {
                            locationsContainingAntinode.Add(new Point(antinodeX, antinodeY));
                        }
                    }
                }
            }
        }

        return locationsContainingAntinode.Count;
    }
}

[TestFixture]
internal class Day8Tests
{
    [Test]
    public void Day8Task1Example()
    {
        string[] input =
        {
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............"
        };

        Day8.GetNumberOfLocationsContainingAntinode(input).Should().Be(14);
    }

    [Test]
    public void Day8Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day8.txt");
        var result = Day8.GetNumberOfLocationsContainingAntinode(input);
        result.Should().Be(291);
    }

    [Test]
    public void Day8Task2Example()
    {
        string[] input =
        {
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............"
        };

        Day8.GetNumberOfLocationsContainingAntinodeWithUpdatedModel(input).Should().Be(34);
    }

    [Test]
    public void Day8Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day8.txt");
        var result = Day8.GetNumberOfLocationsContainingAntinodeWithUpdatedModel(input);
        result.Should().Be(1015);
    }
}