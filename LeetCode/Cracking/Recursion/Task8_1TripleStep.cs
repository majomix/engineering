using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// A child is running up a staircase with n steps and can hop either 1, 2 or 3 steps at a time.
/// Implement a method to count how many possible ways the child can run up the stairs.
///
/// Solution:
/// Memoization: Example of repeating sub-problem happens after initial hops 1-2 vs 2-1.
/// </summary>
internal class Task8_1TripleStep
{
    public long CountWays(int steps)
    {
        var memo = new Dictionary<int, long>();

        return GetSteps(steps, memo);
    }

    private long GetSteps(int steps, Dictionary<int, long> memo)
    {
        if (steps < 1)
            return 0;

        if (steps == 1)
            return 1;

        if (memo.ContainsKey(steps))
            return memo[steps];

        var waysToGo = 0L;

        waysToGo += GetSteps(steps - 1, memo);
        waysToGo += GetSteps(steps - 2, memo);
        waysToGo += GetSteps(steps - 3, memo);

        memo[steps] = waysToGo;

        return waysToGo;
    }
}

[TestFixture]
public class Task8_1TripleStepTests
{
    private static object[] testCases =
    {
        new object[] { 1, 1 },
        new object[] { 2, 1 },
        new object[] { 10, 149 },
        new object[] { 37, 2082876103 }
    };

    [TestCaseSource(nameof(testCases))]
    public void CountWaysTest(int steps, long expectedResult)
    {
        // arrange
        var sut = new Task8_1TripleStep();

        // act
        var result = sut.CountWays(steps);

        // assert
        result.Should().Be(expectedResult);
    }
}
