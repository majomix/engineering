using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.Recursion;

/// <summary>
/// Given boolean expression consisting of the symbols 0, 1, &, | and ^ and a desired boolean result value, implement a function to count the number of ways
/// of parenthesizing the expression such that it evaluates to result. The expression should be fully parenthesized but not extraneously.
/// 
/// Solution:
/// Implemented as suggested by the book pg. 368.
/// </summary>
internal class Task8_14BooleanEvaluation
{
    public int GetNumberOfWaysToParenthesize(string expression, bool evaluationResult)
    {
        if (expression.Length == 0)
            return 0;

        if (expression.Length == 1)
            return (expression == "0" && !evaluationResult) || (expression == "1" && evaluationResult) ? 1 : 0;

        int ways = 0;

        for (var i = 1; i < expression.Length; i += 2)
        {
            var left = expression.Substring(0, i);
            var right = expression.Substring(i + 1, expression.Length - (i + 1));

            var leftTrue = GetNumberOfWaysToParenthesize(left, true);
            var leftFalse = GetNumberOfWaysToParenthesize(left, false);
            var rightTrue = GetNumberOfWaysToParenthesize(right, true);
            var rightFalse = GetNumberOfWaysToParenthesize(right, false);
            var total = (leftTrue + leftFalse) * (rightTrue + rightFalse);

            var totalTrue = 0;
            var currentCharacter = expression[i];

            switch (currentCharacter)
            {
                case '^':
                    totalTrue = leftTrue * rightFalse + leftFalse * rightTrue;
                    break;
                case '|':
                    totalTrue = leftTrue * rightTrue + leftTrue * rightFalse + leftFalse * rightTrue;
                    break;
                case '&':
                    totalTrue = leftTrue * rightTrue;
                    break;
            }

            ways += evaluationResult ? totalTrue : total - totalTrue;
        }

        return ways;
    }
}

[TestFixture]
public class Task8_14BooleanEvaluationTests
{
    private static object[] testCases =
    {
        new object[] { "1^0|0|1", false, 2 },
        new object[] { "0&0&0&1^1|0", true, 10 }
    };

    [TestCaseSource(nameof(testCases))]
    public void GetNumberOfWaysToParenthesizeTest(string expression, bool evaluationResult, int expectedResult)
    {
        // arrange
        var sut = new Task8_14BooleanEvaluation();

        // act
        var result = sut.GetNumberOfWaysToParenthesize(expression, evaluationResult);

        // assert
        result.Should().Be(expectedResult);
    }
}