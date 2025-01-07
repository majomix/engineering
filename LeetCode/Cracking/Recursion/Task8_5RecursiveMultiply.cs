using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Write a recursive function to multiply two positive integers withouzt using the * or / operator.
/// You can use addition, subtraction, and bit shifting, but you should minimize the number of those operations.
///
/// Solution:
/// 1. Naive recursive approach taking O(s) where s is the smaller of two numbers.
/// 2. Halving recursive approach taking O(log(s)) where s is the smaller of two numbers.
/// </summary>
internal class Task8_5RecursiveMultiply
{
    public int MultiplyRecursivelyNaive(int a, int b)
    {
        var bigger = a > b ? a : b;
        var smaller = a > b ? b : a;

        return MultiplyRecursivelyNaiveInner(bigger, smaller);
    }

    public int MultiplyRecursivelyNaiveInner(int bigger, int smaller)
    {
        if (smaller == 0)
            return 0;

        return MultiplyRecursivelyNaiveInner(smaller - 1, bigger) + bigger;
    }

    public int MultiplyRecursivelyThroughGridReduction(int a, int b)
    {
        var bigger = a > b ? a : b;
        var smaller = a > b ? b : a;

        return MultiplyRecursivelyThroughGridReductionInner(bigger, smaller);
    }

    private int MultiplyRecursivelyThroughGridReductionInner(int bigger, int smaller)
    {
        if (smaller == 0)
            return 0;

        if (smaller == 1)
            return bigger;

        var half = smaller >> 1;
        var result = MultiplyRecursivelyThroughGridReductionInner(bigger, half);

        var final = result + result;
        if (smaller % 2 != 0)
        {
            final += bigger;
        }

        return final;
    }
}

[TestFixture]
public class Task8_5RecursiveMultiplyTests
{
    private static object[] testCases =
    {
        new[] { 1, 5, 5 },
        new[] { 5, 5, 25 },
        new[] { 10, 5, 50 },
        new[] { 0, 5, 0 }
    };

    [TestCaseSource(nameof(testCases))]
    public void MultiplyRecursivelyNaiveTest(int a, int b, int expectedResult)
    {
        // arrange
        var sut = new Task8_5RecursiveMultiply();

        // act
        var result = sut.MultiplyRecursivelyNaive(a, b);

        // assert
        result.Should().Be(expectedResult);
    }

    [TestCaseSource(nameof(testCases))]
    public void MultiplyRecursivelyThroughGridReductionTest(int a, int b, int expectedResult)
    {
        // arrange
        var sut = new Task8_5RecursiveMultiply();

        // act
        var result = sut.MultiplyRecursivelyThroughGridReduction(a, b);

        // assert
        result.Should().Be(expectedResult);
    }
}
