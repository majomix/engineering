using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day2
{
    /// <summary>
    /// The unusual data consists of many reports, one report per line. Each report is a list of numbers called levels that are separated by spaces.
    ///
    /// The engineers are trying to figure out which reports are safe. The Red-Nosed reactor safety systems can only tolerate levels that are either
    /// gradually increasing or gradually decreasing. So, a report only counts as safe if both of the following are true:
    /// 1. The levels are either all increasing or all decreasing.
    /// 2. Any two adjacent levels differ by at least one and at most three.
    ///
    /// Analyze the unusual data from the engineers. How many reports are safe?
    /// </summary>
    public static int GetNumberOfSafeReports(string[] input)
    {
        var safeReportsCount = 0;

        foreach (var report in input)
        {
            var split = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 0)
                continue;

            var levels = split.Select(int.Parse).ToList();

            var isUnsafe = IsReportUnsafe(levels);

            if (!isUnsafe)
            {
                safeReportsCount++;
            }
        }

        return safeReportsCount;
    }

    private static bool IsReportUnsafe(List<int> levels)
    {
        var shouldIncrease = levels[1] > levels[0];
        for (var i = 1; i < levels.Count; i++)
        {
            if (shouldIncrease && levels[i] <= levels[i - 1])
            {
                return true;
            }

            if (!shouldIncrease && levels[i] >= levels[i - 1])
            {
                return true;
            }

            if (Math.Abs(levels[i] - levels[i - 1]) > 3)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// The Problem Dampener is a reactor-mounted module that lets the reactor safety systems tolerate a single bad level in what would otherwise be a safe report.
    /// It's like the bad level never happened!
    ///
    /// Now, the same rules apply as before, except if removing a single level from an unsafe report would make it safe, the report instead counts as safe.
    ///
    /// Update your analysis by handling situations where the Problem Dampener can remove a single level from unsafe reports. How many reports are now safe?
    /// </summary>
    public static int GetNumberOfSafeReportsWithProblemDampener(string[] input)
    {
        var safeReportsCount = 0;

        foreach (var report in input)
        {
            var split = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length == 0)
                continue;

            var levels = split.Select(int.Parse).ToList();

            for (var i = 0; i < levels.Count; i++)
            {
                var dampenedLevels = new List<int>();

                for (var k = 0; k < levels.Count; k++)
                {
                    if (k != i)
                    {
                        dampenedLevels.Add(levels[k]);
                    }
                }

                if (!IsReportUnsafe(dampenedLevels))
                {
                    safeReportsCount++;
                    break;
                }
            }
        }

        return safeReportsCount;
    }
}

[TestFixture]
internal class Day2Tests
{
    [Test]
    public void Day2Task1Example()
    {
        string[] input =
        {
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        };

        Day2.GetNumberOfSafeReports(input).Should().Be(2);
    }

    [Test]
    public void Day2Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day2.txt");
        var result = Day2.GetNumberOfSafeReports(input);
        result.Should().Be(332);
    }

    [Test]
    public void Day2Task2Example()
    {
        string[] input =
        {
            "7 6 4 2 1",
            "1 2 7 8 9",
            "9 7 6 2 1",
            "1 3 2 4 5",
            "8 6 4 4 1",
            "1 3 6 7 9"
        };

        Day2.GetNumberOfSafeReportsWithProblemDampener(input).Should().Be(4);
    }

    [Test]
    public void Day2Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day2.txt");
        var result = Day2.GetNumberOfSafeReportsWithProblemDampener(input);
        result.Should().Be(398);
    }
}
