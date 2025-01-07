using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day11
{
    /// <summary>
    /// You've noticed a strange set of physics-defying stones. The strange part is that every time you blink, the stones change.
    /// 
    /// As you observe them for a while, you find that the stones have a consistent behavior.
    /// Every time you blink, the stones each simultaneously change according to the first applicable rule in this list:
    /// - If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1
    /// - If the stone is engraved with a number that has an even number of digits, it is replaced by two stones.
    ///   The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved
    ///   on the new right stone.
    ///   (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.
    /// - If none of the other rules apply, the stone is replaced by a new stone;
    ///   the old stone's number multiplied by 2024 is engraved on the new stone.
    /// No matter how the stones change, their order is preserved, and they stay on their perfectly straight line.
    /// 
    /// How many stones will you have after blinking 25 times?
    /// </summary>
    public static List<long> GetNumberOfStonesAfterBlinking(string inputLine, int blinks)
    {
        var stones = inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        for (var i = 0; i < blinks; i++)
        {
            stones = TransformStones(stones);
        }

        return stones;
    }

    private static List<long> TransformStones(List<long> stones)
    {
        var result = new List<long>();

        foreach (var stone in stones)
        {
            result.AddRange(ProcessStone(stone));
        }

        return result;
    }

    private static List<long> ProcessStone(long stone)
    {
        var result = new List<long>();

        if (stone == 0)
        {
            result.Add(1);
        }
        else
        {
            var digits = GetDigitsFromNumber(stone);
            if (digits.Count % 2 == 0)
            {
                var splitPoint = digits.Count / 2;
                var leftStone = GetNumberFromDigits(digits, 0, splitPoint);
                var rightStone = GetNumberFromDigits(digits, splitPoint, digits.Count);
                result.Add(leftStone);
                result.Add(rightStone);
            }
            else
            {
                result.Add(stone * 2024);
            }
        }

        return result;
    }

    private static List<int> GetDigitsFromNumber(long number)
    {
        var digits = new List<int>();

        while (number > 0)
        {
            digits.Add((int)(number % 10));
            number /= 10;
        }

        digits.Reverse();

        return digits.ToList();
    }

    private static long GetNumberFromDigits(List<int> digits, int start, int end)
    {
        var result = 0L;

        var multiplier = 1;
        for (var i = end - 1; i >= start; i--)
        {
            result += digits[i] * multiplier;
            multiplier *= 10;
        }

        return result;
    }

    /// <summary>
    /// How many stones will you have after blinking 25 times?
    /// </summary>
    public static long GetNumberOfStonesAfterBlinkingRecursively(string input, int iterations)
    {
        var stones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        var memo = new Dictionary<(long, long), long>();
        var numberOfStones = 0L;
        
        foreach (var stone in stones)
        {
            numberOfStones += GetNumberOfStonesAfterBlinkingRecursively(stone, iterations, memo);
        }

        return numberOfStones;
    }

    private static long GetNumberOfStonesAfterBlinkingRecursively(long stone, int iterationsToGo, Dictionary<(long, long), long> memo)
    {
        if (iterationsToGo == 0)
            return 1;

        if (memo.ContainsKey((stone, iterationsToGo)))
            return memo[(stone, iterationsToGo)];

        var numberOfStones = 0L;

        foreach (var modifiedStone in ProcessStone(stone))
        {
            numberOfStones += GetNumberOfStonesAfterBlinkingRecursively(modifiedStone, iterationsToGo - 1, memo);
        }

        memo[(stone, iterationsToGo)] = numberOfStones;

        return numberOfStones;
    }
}

[TestFixture]
internal class Day11Tests
{
    [TestCase("0 1 10 99 999", 1, "1 2024 1 0 9 9 2021976")]
    [TestCase("125 17", 1, "253000 1 7")]
    [TestCase("125 17", 2, "253 0 2024 14168")]
    [TestCase("125 17", 3, "512072 1 20 24 28676032")]
    [TestCase("125 17", 4, "512 72 2024 2 0 2 4 2867 6032")]
    [TestCase("125 17", 5, "1036288 7 2 20 24 4048 1 4048 8096 28 67 60 32")]
    [TestCase("125 17", 6, "2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2")]
    public void Day11Task1Example1(string input, int blinks, string expectedResult)
    {
        string.Join(" ", Day11.GetNumberOfStonesAfterBlinking(input, blinks)).Should().Be(expectedResult);
    }

    [Test]
    public void Day11Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceText("AdventOfCode._2024.day11.txt");
        var result = Day11.GetNumberOfStonesAfterBlinkingRecursively(input, 25);
        result.Should().Be(217812);
    }

    [Test]
    public void Day11Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceText("AdventOfCode._2024.day11.txt");
        var result = Day11.GetNumberOfStonesAfterBlinkingRecursively(input, 75);
        result.Should().Be(259112729857522);
    }
}