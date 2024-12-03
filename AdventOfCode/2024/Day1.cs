using DataStructures.Heap;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day1
{
    /// <summary>
    /// Pair up the smallest number in the left list with the smallest number in the right list, then the second-smallest left number with the second-smallest right number, and so on.
    /// Within each pair, figure out how far apart the two numbers are; you'll need to add up all of those distances.
    ///
    /// For example, if you pair up a 3 from the left list with a 7 from the right list, the distance apart is 4; if you pair up a 9 with a 3, the distance apart is 6.
    ///
    /// What is the total distance between your lists?
    /// </summary>
    public static int GetTotalDistance(string[] input)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        foreach (var line in input)
        {
            var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                continue;
            
            list1.Add(int.Parse(split[0]));
            list2.Add(int.Parse(split[1]));
        }

        var list1sorted = new CustomMinHeapByDynamicArray<int, int>();
        list1sorted.BuildHeap(list1.ToArray(), value => value);
        var list2sorted = new CustomMinHeapByDynamicArray<int, int>();
        list2sorted.BuildHeap(list2.ToArray(), value => value);

        var totalDistance = 0;
        for (var i = 0; i < list1.Count; i++)
        {
            totalDistance += Math.Abs(list1sorted.ExtractMin() - list2sorted.ExtractMin());
        }

        return totalDistance;
    }

    /// <summary>
    /// This time, you'll need to figure out exactly how often each number from the left list appears in the right list.
    /// Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.
    ///
    /// What is their similarity score?
    /// </summary>
    public static int GetTotalSimilarityScore(string[] input)
    {
        var histogram = new Dictionary<int, int>();

        var list = new List<int>();
        foreach (var line in input)
        {
            var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                continue;

            var leftListValue = int.Parse(split[0]);
            var rightListValue = int.Parse(split[1]);

            list.Add(leftListValue);

            if (!histogram.TryAdd(rightListValue, 1))
            {
                histogram[rightListValue]++;
            }
        }

        var totalSimilarityScore = 0;
        foreach (var value in list)
        {
            histogram.TryGetValue(value, out var histogramCount);
            totalSimilarityScore += value * histogramCount;
        }

        return totalSimilarityScore;
    }
}

[TestFixture]
internal class Day1Tests
{
    [Test]
    public void Day1Task1Example()
    {
        string[] input =
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };

        Day1.GetTotalDistance(input).Should().Be(11);
    }

    [Test]
    public void Day1Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day1.txt");
        var result = Day1.GetTotalDistance(input);
        result.Should().Be(1830467);
    }

    [Test]
    public void Day1Task2Example()
    {
        string[] input =
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };

        Day1.GetTotalSimilarityScore(input).Should().Be(31);
    }

    [Test]
    public void Day1Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day1.txt");
        var result = Day1.GetTotalSimilarityScore(input);
        result.Should().Be(26674158);
    }
}
