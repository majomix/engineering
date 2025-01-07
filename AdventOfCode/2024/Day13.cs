using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace AdventOfCode._2024;

internal static class Day13
{
    /// <summary>
    /// 
    /// </summary>
    public static int Task1(string[] input)
    {
        return 0;
    }

    /// <summary>
    ///
    /// </summary>
    public static int Task2(string[] input)
    {
        return 0;
    }
}

[TestFixture]
internal class Day13Tests
{
    [Test]
    public void Day13Task1Example()
    {
        string[] input =
        {
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        };

        Day13.Task1(input).Should().Be(480);
    }

    [Test]
    public void Day13Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
        var result = Day13.Task1(input);
        result.Should().Be(0);
    }

    [Test]
    public void Day13Task2Example()
    {
        string[] input =
        {

        };

        Day13.Task2(input).Should().Be(0);
    }

    [Test]
    public void Day13Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day13.txt");
        var result = Day13.Task2(input);
        result.Should().Be(0);
    }
}
