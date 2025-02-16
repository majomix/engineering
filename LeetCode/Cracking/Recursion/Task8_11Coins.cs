using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Given an infinite amount of quarters (25 cents), dimes (10 cents), nickels (5 cents), and pennies (1 cent), write code to calculate the number of ways
/// of representing n cents.
/// 
/// Solution:
/// Recursive level with as many levels as there are type of coins. For each level, try adding coins until the reminder is positive. Then pass to lower level.
/// Overlapping sub-problem clear when drawing a picture for case 25. The memo key is compound - reminder and level.
/// </summary>
internal class Task8_11Coins
{
    private readonly List<int> _centValues = new() { 25, 10, 5, 1 };
    private readonly Dictionary<(int, int), long> _memo = new();

    public long GetNumberOfWays(int target)
    {
        return GetNumberOfWaysRecursively(target, 0);
    }

    private long GetNumberOfWaysRecursively(int target, int currentLevel)
    {
        var memoKey = (target, currentLevel);

        if (_memo.TryGetValue(memoKey, out var result))
            return result;

        if (target == 0)
            return 1;

        if (currentLevel == _centValues.Count)
            return 0;

        var currentCoinValue = _centValues[currentLevel];

        var numberOfWays = 0L;
        for (var remainingTarget = target; remainingTarget >= 0; remainingTarget -= currentCoinValue)
        {
            numberOfWays += GetNumberOfWaysRecursively(remainingTarget, currentLevel + 1);
        }

        _memo[memoKey] = numberOfWays;

        return numberOfWays;
    }
}

[TestFixture]
public class Task8_11CoinsTests
{
    private static object[] testCases =
    {
        new object[] { 0, 1 },
        new object[] { 1, 1 },
        new object[] { 5, 2 },
        new object[] { 10, 4 },
        new object[] { 25, 13 },
        new object[] { 30, 18 },
        new object[] { 50, 49 },
        new object[] { 100, 242 }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetNumberOfWaysTest(int target, int expectedResult)
    {
        // arrange
        var sut = new Task8_11Coins();

        // act
        var result = sut.GetNumberOfWays(target);

        // assert
        result.Should().Be(expectedResult);
    }
}