using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Assume you have a method IsSubstring which checks if one word is a substring of another.
    ///
    /// Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one call to IsSubstring.
    ///
    /// Trick/Solution:
    /// * s1s1 will contain any rotation
    /// </summary>
    internal class Task1_9StringRotation
    {
        public bool IsStringRotation(string input, string rotation)
        {
            if (input.Length != rotation.Length)
                return false;

            return $"{input}{input}".Contains(rotation);
        }
    }

    [TestFixture]
    public class Task1_9StringRotationTests
    {
        private static object[] testCases =
        {
            new object[] { "waterbottle", "erbottlewat", true },
            new object[] { "waterbottle", "erbottlewet", false },
            new object[] { "abcdefgh", "habcdefg", true },
            new object[] { "aaaaaaa", "aaaaaaa", true },
        };

        [TestCaseSource(nameof(testCases))]
        public void IsStringRotationTest(string input, string rotation, bool expectedResult)
        {
            // arrange
            var sut = new Task1_9StringRotation();

            // act
            var result = sut.IsStringRotation(input, rotation);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
