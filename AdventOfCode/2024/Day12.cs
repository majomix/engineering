using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace AdventOfCode._2024;

internal static class Day12
{
    /// <summary>
    /// Elves ask if you can lend a hand. They'd like to set up fences around each region of garden plots,
    /// but they can't figure out how much fence they need to order or how much it will cost.
    /// They hand you a map of the garden plots.
    /// 
    /// Each garden plot grows only a single type of plant and is indicated by a single letter on your map.
    /// When multiple garden plots are growing the same type of plant and are touching (horizontally or vertically),
    /// they form a region.
    /// 
    /// In order to accurately calculate the cost of the fence around a single region, you need to know that
    /// region's area and perimeter.
    /// The area of a region is simply the number of garden plots the region contains.
    /// Each garden plot is a square and so has four sides. The perimeter of a region is the number of sides
    /// of garden plots in the region that do not touch another garden plot in the same region.
    /// 
    /// Plants of the same type can appear in multiple separate regions, and regions can even appear within other regions.
    /// 
    /// Due to "modern" business practices, the price of fence required for a region is found by multiplying that
    /// region's area by its perimeter. The total price of fencing all regions on a map is found by adding together
    /// the price of fence for every region on the map.
    /// 
    /// What is the total price of fencing all regions on your map?
    /// </summary>
    public static int GetTotalPriceOfFencing(string[] input)
    {
        var priceOfFencing = 0;

        var regions = ParseInputToRegions(input);

        foreach (var region in regions)
        {
            priceOfFencing += region.GetArea() * region.GetPerimeter();
        }

        return priceOfFencing;
    }

    private static List<Region> ParseInputToRegions(string[] input)
    {
        var allPlants = new Dictionary<Point, Plant>();
        var regions = new List<Region>();
        var mutableInput = new List<List<char>>();

        for (var row = 0; row < input.Length; row++)
        {
            var rowList = new List<char>();
            for (var column = 0; column < input[row].Length; column++)
            {
                rowList.Add(input[row][column]);
            }
            mutableInput.Add(rowList);
        }

        for (var row = 0; row < mutableInput.Count; row++)
        {
            for (var column = 0; column < mutableInput[row].Count; column++)
            {
                if (mutableInput[row][column] != '.')
                {
                    var currentPlantType = input[row][column];
                    var region = new Region(currentPlantType);
                    ExtractRegion(mutableInput, new Point(column, row), region, allPlants);
                    regions.Add(region);
                }
            }
        }

        return regions;
    }

    private static void ExtractRegion(List<List<char>> input, Point currentPoint, Region region, Dictionary<Point, Plant> allPlants)
    {
        if (allPlants.ContainsKey(currentPoint))
            return;

        if (!IsInsideBounds(input, currentPoint))
            return;

        if (input[currentPoint.Y][currentPoint.X] != region.PlantType)
            return;

        var plant = new Plant(currentPoint, region.PlantType, allPlants);

        allPlants.Add(currentPoint, plant);
        input[currentPoint.Y][currentPoint.X] = '.';
            
        var options = new List<Point>
        {
            new Point(currentPoint.X, currentPoint.Y - 1), // up
            new Point(currentPoint.X, currentPoint.Y + 1), // down
            new Point(currentPoint.X - 1, currentPoint.Y), // left
            new Point(currentPoint.X + 1, currentPoint.Y) // right
        };

        region.Plants.Add(plant);

        foreach (var option in options)
        {
            ExtractRegion(input, option, region, allPlants);
        }
    }

    private static bool IsInsideBounds(List<List<char>> input, Point point)
    {
        if (point.X < 0 || point.Y < 0)
            return false;

        if (point.Y >= input.Count)
            return false;

        if (point.X >= input[0].Count)
            return false;

        return true;
    }

    /// <summary>
    /// Fortunately, the Elves are trying to order so much fence that they qualify for a bulk discount!
    /// 
    /// Under the bulk discount, instead of using the perimeter to calculate the price, you need to use the number of sides each
    /// region has. Each straight section of fence counts as a side, regardless of how long it is.
    /// 
    /// What is the new total price of fencing all regions on your map?
    /// </summary>
    public static int GetTotalPriceOfFencingWithPerSidePrice(string[] input)
    {
        var priceOfFencing = 0;

        var regions = ParseInputToRegions(input);

        foreach (var region in regions)
        {
            priceOfFencing += region.GetArea() * region.GetSides();
        }

        return priceOfFencing;
    }

    public class Region
    {
        public List<Plant> Plants = new();

        public char PlantType { get; }

        public Region(char plantType)
        {
            PlantType = plantType;
        }

        public int GetArea()
        {
            return Plants.Count;
        }

        public int GetPerimeter()
        {
            var perimeter = 0;

            foreach (var plant in Plants)
            {
                perimeter += plant.GetOutlineSegments().Count;
            }

            return perimeter;
        }

        public int GetSides()
        {
            var lines = 0;
            var segments = new HashSet<OutlineSegment>();

            foreach (var plant in Plants)
            {
                foreach (var segment in plant.GetOutlineSegments())
                {
                    segments.Add(segment);
                }
            }

            while (segments.Count > 0)
            {
                var segment = segments.First();

                if (segment.Side == OutlineSide.Up || segment.Side == OutlineSide.Down)
                {
                    var examinedSegment = new OutlineSegment(new Point(segment.Point.X - 1, segment.Point.Y), segment.Side);
                    while (segments.Contains(examinedSegment))
                    {
                        segments.Remove(examinedSegment);
                        examinedSegment = new OutlineSegment(new Point(examinedSegment.Point.X - 1, examinedSegment.Point.Y), examinedSegment.Side);
                    }

                    examinedSegment = new OutlineSegment(new Point(segment.Point.X + 1, segment.Point.Y), segment.Side);
                    while (segments.Contains(examinedSegment))
                    {
                        segments.Remove(examinedSegment);
                        examinedSegment = new OutlineSegment(new Point(examinedSegment.Point.X + 1, examinedSegment.Point.Y), examinedSegment.Side);
                    }
                }
                else
                {
                    var examinedSegment = new OutlineSegment(new Point(segment.Point.X, segment.Point.Y - 1), segment.Side);
                    while (segments.Contains(examinedSegment))
                    {
                        segments.Remove(examinedSegment);
                        examinedSegment = new OutlineSegment(new Point(examinedSegment.Point.X, examinedSegment.Point.Y - 1), examinedSegment.Side);
                    }

                    examinedSegment = new OutlineSegment(new Point(segment.Point.X, segment.Point.Y + 1), segment.Side);
                    while (segments.Contains(examinedSegment))
                    {
                        segments.Remove(examinedSegment);
                        examinedSegment = new OutlineSegment(new Point(examinedSegment.Point.X, examinedSegment.Point.Y + 1), examinedSegment.Side);
                    }
                }

                lines++;
                segments.Remove(segment);
            }

            return lines;
        }
    }

    public class Plant
    {
        public Point Position { get; }

        private readonly Dictionary<Point, Plant> _allPlants;
        private readonly char _plant;

        public Plant(Point point, char plant, Dictionary<Point, Plant> allPlants)
        {
            Position = point;

            _allPlants = allPlants;
            _plant = plant;
        }

        public List<OutlineSegment> GetOutlineSegments()
        {
            var result = new List<OutlineSegment>();

            var options = new List<OutlineSegment>
            {
                new OutlineSegment(new Point(Position.X, Position.Y - 1), OutlineSide.Up),
                new OutlineSegment(new Point(Position.X, Position.Y + 1), OutlineSide.Down),
                new OutlineSegment(new Point(Position.X - 1, Position.Y), OutlineSide.Left),
                new OutlineSegment(new Point(Position.X + 1, Position.Y), OutlineSide.Right)
            };

            foreach (var option in options)
            {
                _allPlants.TryGetValue(option.Point, out var plantOnMap);
                if (plantOnMap == null || plantOnMap._plant != _plant)
                {
                    result.Add(option);
                }
            }

            return result;
        }
    }

    public record OutlineSegment(Point Point, OutlineSide Side);

    public enum OutlineSide
    {
        Up,
        Down,
        Left,
        Right
    }
}

[TestFixture]
internal class Day12Tests
{
    [Test]
    public void Day12Task1Example1()
    {
        string[] input =
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC"
        };

        Day12.GetTotalPriceOfFencing(input).Should().Be(140);
    }

    [Test]
    public void Day12Task1Example2()
    {
        string[] input =
        {
            "OOOOO",
            "OXOXO",
            "OOOOO",
            "OXOXO",
            "OOOOO"
        };

        Day12.GetTotalPriceOfFencing(input).Should().Be(772);
    }

    [Test]
    public void Day12Task1Example3()
    {
        string[] input =
        {
            "RRRRIICCFF",
            "RRRRIICCCF",
            "VVRRRCCFFF",
            "VVRCCCJFFF",
            "VVVVCJJCFE",
            "VVIVCCJJEE",
            "VVIIICJJEE",
            "MIIIIIJJEE",
            "MIIISIJEEE",
            "MMMISSJEEE"
        };

        Day12.GetTotalPriceOfFencing(input).Should().Be(1930);
    }

    [Test]
    public void Day12Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day12.txt");
        var result = Day12.GetTotalPriceOfFencing(input);
        result.Should().Be(1352976);
    }

    [Test]
    public void Day12Task2Example1()
    {
        string[] input =
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC"
        };

        Day12.GetTotalPriceOfFencingWithPerSidePrice(input).Should().Be(80);
    }

    [Test]
    public void Day12Task2Example2()
    {
        string[] input =
        {
            "EEEEE",
            "EXXXX",
            "EEEEE",
            "EXXXX",
            "EEEEE"
        };

        Day12.GetTotalPriceOfFencingWithPerSidePrice(input).Should().Be(236);
    }

    [Test]
    public void Day12Task2Example3()
    {
        string[] input =
        {
            "AAAAAA",
            "AAABBA",
            "AAABBA",
            "ABBAAA",
            "ABBAAA",
            "AAAAAA"
        };

        Day12.GetTotalPriceOfFencingWithPerSidePrice(input).Should().Be(368);
    }

    [Test]
    public void Day12Task2Example4()
    {
        string[] input =
        {
            "RRRRIICCFF",
            "RRRRIICCCF",
            "VVRRRCCFFF",
            "VVRCCCJFFF",
            "VVVVCJJCFE",
            "VVIVCCJJEE",
            "VVIIICJJEE",
            "MIIIIIJJEE",
            "MIIISIJEEE",
            "MMMISSJEEE"
        };

        Day12.GetTotalPriceOfFencingWithPerSidePrice(input).Should().Be(1206);
    }

    [Test]
    public void Day12Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day12.txt");
        var result = Day12.GetTotalPriceOfFencingWithPerSidePrice(input);
        result.Should().Be(808796);
    }
}