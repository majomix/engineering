using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode._2024;

internal static class Day3
{
    /// <summary>
    /// The computer appears to be trying to run a program, but its memory is corrupted. All of the instructions have been jumbled up!
    ///
    /// It seems like the goal of the program is just to multiply some numbers. It does that with instructions like mul(X,Y), where X and Y are each 1-3 digit numbers.
    /// For instance, mul(44,46) multiplies 44 by 46 to get a result of 2024. Similarly, mul(123,4) would multiply 123 by 4.
    ///
    /// However, because the program's memory has been corrupted, there are also many invalid characters that should be ignored, even if they look like part of a mul instruction.
    /// Sequences like mul(4*, mul(6,9!, ?(12,34), or mul ( 2 , 4 ) do nothing.
    ///
    /// Scan the corrupted memory for uncorrupted mul instructions. What do you get if you add up all of the results of the multiplications?
    /// </summary>
    public static int GetResultOfMultiplication(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            var regex = new Regex(@"mul\(\d{1,3}\,\d{1,3}\)");
            var matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                var split = match.Value.Split(new [] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 3)
                {
                    var firstNumber = int.Parse(split[1]);
                    var secondNumber = int.Parse(split[2]);
                    var multiply = firstNumber * secondNumber;
                    result += multiply;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// As you scan through the corrupted memory, you notice that some of the conditional statements are also still intact.
    /// If you handle some of the uncorrupted conditional statements in the program, you might be able to get an even more accurate result.
    ///
    /// There are two new instructions you'll need to handle:
    /// The do() instruction enables future mul instructions
    /// The don't() instruction disables future mul instructions.
    ///
    /// Only the most recent do() or don't() instruction applies. At the beginning of the program, mul instructions are enabled.
    ///
    /// What do you get if you add up all of the results of just the enabled multiplications?
    /// </summary>
    public static int GetResultOfMultiplicationWithEnablers(string[] input)
    {
        var result = 0;

        var isEnabled = true;
        foreach (var line in input)
        {
            var regex = new Regex(@"mul\(\d{1,3}\,\d{1,3}\)|do\(\)|don\'t\(\)");
            var matches = regex.Matches(line);
            foreach (Match match in matches)
            {
                var value = match.Value;
                if (value == "do()")
                {
                    isEnabled = true;
                }
                else if (value == "don't()")
                {
                    isEnabled = false;
                }
                else if (isEnabled)
                {
                    var split = match.Value.Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 3)
                    {
                        var firstNumber = int.Parse(split[1]);
                        var secondNumber = int.Parse(split[2]);
                        var multiply = firstNumber * secondNumber;
                        result += multiply;
                    }
                }
            }
        }

        return result;
    }
}

[TestFixture]
internal class Day3Tests
{
    [Test]
    public void Day3Task1Example()
    {
        string[] input =
        {
            "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
        };

        Day3.GetResultOfMultiplication(input).Should().Be(161);
    }

    [Test]
    public void Day3Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day3.txt");
        var result = Day3.GetResultOfMultiplication(input);
        result.Should().Be(187194524);
    }

    [Test]
    public void Day3Task2Example()
    {
        string[] input =
        {
            "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
        };

        Day3.GetResultOfMultiplicationWithEnablers(input).Should().Be(48);
    }

    [Test]
    public void Day3Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day3.txt");
        var result = Day3.GetResultOfMultiplicationWithEnablers(input);
        result.Should().Be(127092535);
    }
}