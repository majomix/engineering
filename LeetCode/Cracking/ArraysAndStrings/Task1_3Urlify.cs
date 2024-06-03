using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Write a method to replace all spaces in a string with %20. You may assume that the string has sufficient space at the end to hold the additional characters
    /// and that you are given true length of the string.
    /// 
    /// Solution:
    /// * go backwards to allow in-place re-write
    /// * do not expect the target array will exactly fit the requirement; it may be longer
    /// </summary>
    internal class Task1_3Urlify
    {
        public char[] Urlify(char[] input, int length)
        {
            var numberOfSpaces = 0;
            for (var i = 0; i < length; i++)
            {
                if (input[i] == ' ')
                    numberOfSpaces++;
            }

            var finalLength = length + 2 * numberOfSpaces;

            var writePointer = finalLength - 1;
            for (var i = length - 1; i >= 0; i--)
            {
                if (input[i] == ' ')
                {
                    input[writePointer--] = '0';
                    input[writePointer--] = '2';
                    input[writePointer--] = '%';
                }
                else
                {
                    input[writePointer--] = input[i];
                }
            }

            return input;
        }
    }

    [TestFixture]
    public class Task1_3UrlifyTests
    {
        private static object[] testCases =
        {
            new object[] { "Mr John Smith    ".ToCharArray(), 13, "Mr%20John%20Smith".ToCharArray() },
            new object[] { "Mr John Smith                        ".ToCharArray(), 13, "Mr%20John%20Smith                    ".ToCharArray() }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationHashMapTest(char[] input, int length, char[] output)
        {
            // arrange
            var sut = new Task1_3Urlify();

            // act
            var result = sut.Urlify(input, length);

            // assert
            result.Should().BeEquivalentTo(output);
        }
    }
}
