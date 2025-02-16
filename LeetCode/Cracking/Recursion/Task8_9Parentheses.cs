using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Implement an algorithm to print all valid combinations of n pairs of parentheses.
/// 
/// Solution:
/// * remember how many pairs are open and how many still need to be closed
/// </summary>
internal class Task8_9Parentheses
{
    public List<string> GetParentheses(int n)
    {
        var result = new List<string>();

        GetParenthesesRecursively(n, 0, 0, string.Empty, result);

        return result;
    }

    private void GetParenthesesRecursively(int count, int open, int closed, string prefix, List<string> result)
    {
        if (count == 0)
            return;

        if (open > count || closed > open)
            return;

        if (closed == count)
        {
            result.Add(prefix);
            return;
        }

        GetParenthesesRecursively(count, open + 1, closed, $"{prefix}(", result);
        GetParenthesesRecursively(count, open, closed + 1, $"{prefix})", result);
    }
}

[TestFixture]
public class Task8_9ParenthesesTests
{
    private static object[] testCases =
    {
        new object[] { 0, new List<string>() },
        new object[] { 1, new List<string> { "()" } },
        new object[] { 2, new List<string> { "()()", "(())" } },
        new object[] { 3, new List<string> { "()()()", "((()))", "()(())", "(())()", "(()())" } }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetPermutationsTest(int input, List<string> expectedResult)
    {
        // arrange
        var sut = new Task8_9Parentheses();

        // act
        var result = sut.GetParentheses(input);

        // assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
