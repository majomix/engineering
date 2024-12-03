using AdventOfCode;
using AdventOfCode.Day1;
using FluentAssertions;
using NUnit.Framework;

var d1p1 = new Day1Puzzle1();
d1p1.Solve();

internal static class DayN
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
internal class DayNTests
{
    [Test]
    public void DayNTask1Example()
    {
        string[] input =
        {
            "",
        };

        DayN.Task1(input).Should().Be(0);
    }

    [Test]
    public void DayNTask1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.dayN.txt");
        var result = DayN.Task1(input);
        result.Should().Be(0);
    }

    [Test]
    public void DayNTask2Example()
    {
        string[] input =
        {

        };

        DayN.Task2(input).Should().Be(0);
    }

    [Test]
    public void DayNTask2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.dayN.txt");
        var result = DayN.Task2(input);
        result.Should().Be(0);
    }
}