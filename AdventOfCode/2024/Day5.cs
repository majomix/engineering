using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day5
{
    /// <summary>
    /// Safety protocols clearly indicate that new pages for the safety manuals must be printed in a very specific order.
    /// The notation X|Y means that if both page number X and page number Y are to be produced as part of an update,
    /// page number X must be printed at some point before page number Y.
    /// 
    /// The Elf has for you both the page ordering rules and the pages to produce in each update, but can't figure out whether each update has the pages in the right order.
    ///
    /// The first section specifies the page ordering rules, one per line.
    /// The first rule, 47|53, means that if an update includes both page number 47 and page number 53,
    /// then page number 47 must be printed at some point before page number 53.
    /// 47 doesn't necessarily need to be immediately before 53; other pages are allowed to be between them.
    ///
    /// For some reason, the Elves also need to know the middle page number of each update being printed.
    /// Because you are currently only printing the correctly-ordered updates, you will need to find the middle page number of each correctly-ordered update.
    ///
    /// Determine which updates are already in the correct order. What do you get if you add up the middle page number from those correctly-ordered updates?
    /// </summary>
    public static int GetSumOfMiddlePageNumbersOfCorrectlyOrderedUpdates(string[] input)
    {
        var sumOfMiddlePageNumbers = 0;
        var dependencies = new Dictionary<int, HashSet<int>>();

        var isParsingUpdates = false;
        foreach (var line in input)
        {
            if (!isParsingUpdates)
            {
                var split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2)
                {
                    ParseDependencyLine(split, dependencies);
                }
                else
                {
                    isParsingUpdates = true;
                }
            }
            else
            {
                var pageNumbers = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                var isCorrect = IsUpdateOrderCorrect(pageNumbers, dependencies);

                if (isCorrect)
                {
                    sumOfMiddlePageNumbers += pageNumbers[pageNumbers.Count / 2];
                }
            }
        }

        return sumOfMiddlePageNumbers;
    }

    private static void ParseDependencyLine(string[] split, Dictionary<int, HashSet<int>> dependencies)
    {
        var dependsOnPage = int.Parse(split[0]);
        var pageToPrint = int.Parse(split[1]);
        dependencies.TryAdd(pageToPrint, new HashSet<int>());
        dependencies[pageToPrint].Add(dependsOnPage);
    }

    private static bool IsUpdateOrderCorrect(List<int> pageNumbers, Dictionary<int, HashSet<int>> dependencies)
    {
        var map = new HashSet<int>();
        foreach (var pageNumber in pageNumbers)
        {
            map.Add(pageNumber);
        }

        if (map.Count == 0)
            return false;

        foreach (var page in pageNumbers)
        {
            if (!dependencies.TryGetValue(page, out var currentPageDependencies))
            {
                map.Remove(page);
                continue;
            }

            foreach (var dependency in currentPageDependencies)
            {
                if (map.Contains(dependency))
                {
                    return false;
                }
            }

            map.Remove(page);
        }

        return true;
    }

    private static void CorrectPageNumbers(List<int> pageNumbers, Dictionary<int, HashSet<int>> dependencies)
    {
        var map = new HashSet<int>();
        foreach (var pageNumber in pageNumbers)
        {
            map.Add(pageNumber);
        }

        for (var i = 0; i < pageNumbers.Count; i++)
        {
            var hasOutOfOrderDependency = true;
            var page = pageNumbers[i];
            if (!dependencies.TryGetValue(page, out var currentPageDependencies))
            {
                map.Remove(page);
                continue;
            }

            foreach (var dependency in currentPageDependencies)
            {
                if (map.Contains(dependency))
                {
                    var dependencyIndex = pageNumbers.IndexOf(dependency);
                    (pageNumbers[i], pageNumbers[dependencyIndex]) = (pageNumbers[dependencyIndex], pageNumbers[i]);
                    i--;
                    hasOutOfOrderDependency = false;
                    break;
                }
            }

            if (hasOutOfOrderDependency)
            {
                map.Remove(page);
            }
        }
    }

    /// <summary>
    /// While the Elves get to work printing the correctly-ordered updates, you have a little time to fix the rest of them.
    /// 
    /// For each of the incorrectly-ordered updates, use the page ordering rules to put the page numbers in the right order.
    ///
    /// Find the updates which are not in the correct order. What do you get if you add up the middle page numbers after correctly ordering just those updates?
    /// </summary>
    public static int GetSumOfMiddlePageNumbersAfterCorrectingUpdates(string[] input)
    {
        var sumOfMiddlePageNumbers = 0;
        var dependencies = new Dictionary<int, HashSet<int>>();

        var isParsingUpdates = false;
        foreach (var line in input)
        {
            if (!isParsingUpdates)
            {
                var split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2)
                {
                    ParseDependencyLine(split, dependencies);
                }
                else
                {
                    isParsingUpdates = true;
                }
            }
            else
            {
                var pageNumbers = line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                var isCorrect = IsUpdateOrderCorrect(pageNumbers, dependencies);

                if (isCorrect || pageNumbers.Count == 0)
                    continue;

                CorrectPageNumbers(pageNumbers, dependencies);

                sumOfMiddlePageNumbers += pageNumbers[pageNumbers.Count / 2];
            }
        }

        return sumOfMiddlePageNumbers;
    }
}

[TestFixture]
internal class Day5Tests
{
    [Test]
    public void Day5Task1Example()
    {
        string[] input =
        {
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47"
        };

        Day5.GetSumOfMiddlePageNumbersOfCorrectlyOrderedUpdates(input).Should().Be(143);
    }

    [Test]
    public void Day5Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day5.txt");
        var result = Day5.GetSumOfMiddlePageNumbersOfCorrectlyOrderedUpdates(input);
        result.Should().Be(7198);
    }

    [Test]
    public void Day5Task2Example()
    {
        string[] input =
        {
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47"
        };

        Day5.GetSumOfMiddlePageNumbersAfterCorrectingUpdates(input).Should().Be(123);
    }

    [Test]
    public void Day5Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day5.txt");
        var result = Day5.GetSumOfMiddlePageNumbersAfterCorrectingUpdates(input);
        result.Should().Be(4230);
    }
}