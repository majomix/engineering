using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Implement a method to perform basic string compression using the counts of repeated characters.
    /// If the compressed string would be larger than the original, then return the original string.
    /// Assume the string has only uppercase and lowercase letters (a-z).
    ///
    /// Discussion:
    /// Run-length encoding: aabccccccaaa -> a2b1c5a3.
    ///
    /// Solution:
    /// * use string builder to prevent string reallocations
    ///
    /// Allocation optimization:
    /// Two runs if we want to determine the optimal string builder capacity upfront.
    /// </summary>
    internal class Task1_6StringCompression
    {
        public string CompressString(string input)
        {
            char current = input[0];
            var currentCount = 1;
            var builder = new StringBuilder();

            for (var i = 1; i < input.Length; i++)
            {
                if (current == input[i])
                {
                    currentCount++;
                    if (i + 1 == input.Length)
                    {
                        builder.Append($"{current}{currentCount}");
                    }
                }
                else
                {
                    builder.Append($"{current}{currentCount}");
                    current = input[i];
                    currentCount = 1;
                }
            }

            var result = builder.ToString();
            return input.Length > result.Length ? result : input;
        }
    }

    [TestFixture]
    public class Task1_6StringCompressionTests
    {
        private static object[] testCases =
        {
            new object[] { "aabcccccaaa", "a2b1c5a3" },
            new object[] { "abcdefghi", "abcdefghi" },
            new object[] { "aaaaaaa", "a7" }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationHashMapTest(string input, string output)
        {
            // arrange
            var sut = new Task1_6StringCompression();

            // act
            var result = sut.CompressString(input);

            // assert
            result.Should().Be(output);
        }
    }
}
