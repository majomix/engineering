using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day7
{
    /// <summary>
    /// Some young elephants were playing nearby and stole all the operators from their calibration equations!
    /// They could finish the calibrations if only someone could determine which test values could possibly be produced
    /// by placing any combination of operators into their calibration equations.
    /// 
    /// Each line represents a single equation. The test value appears before the colon on each line;
    /// it is your job to determine whether the remaining numbers can be combined with operators to produce
    /// the test value.
    /// 
    /// Operators are always evaluated left-to-right, not according to precedence rules.
    /// Furthermore, numbers in the equations cannot be rearranged.
    /// Glancing into the jungle, you can see elephants holding two different types of operators:
    /// add(+) and multiply(*).
    /// 
    /// The engineers just need the total calibration result,
    /// which is the sum of the test values from just the equations that could possibly be true.
    /// 
    /// Determine which equations could possibly be true. What is their total calibration result?
    /// </summary>
    public static long GetTotalCalibrationResult(string[] input)
    {
        var totalCalibrationResult = 0L;

        var operators = new List<Operator> {
            new AdditionOperator(),
            new MultiplyOperator()
        };

        foreach (var equation in input)
        {
            var split = equation.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var target = long.Parse(split[0]);

            if (Recurse(operators, target, long.Parse(split[1]), split, 2))
            {
                totalCalibrationResult += target;
            }
        }

        return totalCalibrationResult;
    }

    private static bool Recurse(List<Operator> operators, long target, long subtotal, string[] operands, int index)
    {
        if (subtotal > target)
            return false;

        if (subtotal == target && index == operands.Length)
            return true;

        if (index >= operands.Length)
            return false;

        var currentOperand = long.Parse(operands[index]);

        var result = false;
        foreach (var op in operators)
        {
            var expressionResult = op.Evaluate(subtotal, currentOperand);

            result |= Recurse(operators, target, expressionResult, operands, index + 1);
        }

        return result;
    }

    /// <summary>
    /// Some well-hidden elephants are holding a third type of operator.
    /// The concatenation operator combines the digits from its left and right inputs into a single number.
    /// Now, apart from the three equations that could be made true using only addition and multiplication, 
    /// there are now three more equations that can be made true by inserting operators.
    /// 
    /// Using your new knowledge of elephant hiding spots, determine which equations could possibly be true.
    /// What is their total calibration result?
    /// </summary>
    public static long GetTotalCalibrationResultWithConcatOperator(string[] input)
    {
        var totalCalibrationResult = 0L;

        var operators = new List<Operator> {
            new AdditionOperator(),
            new MultiplyOperator(),
            new ConcatOperator()
        };

        foreach (var equation in input)
        {
            var split = equation.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var target = long.Parse(split[0]);

            if (Recurse(operators, target, long.Parse(split[1]), split, 2))
            {
                totalCalibrationResult += target;
            }
        }

        return totalCalibrationResult;
    }

    public abstract class Operator
    {
        public abstract long Evaluate(long left, long right);
    }

    public class AdditionOperator : Operator
    {
        public override long Evaluate(long left, long right)
        {
            return left + right;
        }
    }

    public class MultiplyOperator : Operator
    {
        public override long Evaluate(long left, long right)
        {
            return left * right;
        }
    }

    public class ConcatOperator : Operator
    {
        public override long Evaluate(long left, long right)
        {
            var rightCopy = right;
            var rightDigits = 0;

            while (rightCopy > 0)
            {
                rightDigits++;
                rightCopy /= 10;
            }

            return left * (long)Math.Pow(10, rightDigits) + right;
        }
    }
}

[TestFixture]
internal class Day7Tests
{
    [Test]
    public void Day7Task1Example()
    {
        string[] input =
        {
            "190: 10 19",
            "3267: 81 40 27",
            "83: 17 5",
            "156: 15 6",
            "7290: 6 8 6 15",
            "161011: 16 10 13",
            "192: 17 8 14",
            "21037: 9 7 18 13",
            "292: 11 6 16 20"
        };

        Day7.GetTotalCalibrationResult(input).Should().Be(3749);
    }

    [Test]
    public void Day7Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day7.txt");
        var result = Day7.GetTotalCalibrationResult(input);
        result.Should().Be(10741443549536);
    }

    [Test]
    public void Day7Task2Example()
    {
        string[] input =
        {
            "190: 10 19",
            "3267: 81 40 27",
            "83: 17 5",
            "156: 15 6",
            "7290: 6 8 6 15",
            "161011: 16 10 13",
            "192: 17 8 14",
            "21037: 9 7 18 13",
            "292: 11 6 16 20"
        };

        Day7.GetTotalCalibrationResultWithConcatOperator(input).Should().Be(11387);
    }

    [Test]
    public void Day7Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day7.txt");
        var result = Day7.GetTotalCalibrationResultWithConcatOperator(input);
        result.Should().Be(500335179214836);
    }
}