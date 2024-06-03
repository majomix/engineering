using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// There are 3 types of edits performed on strings: insert a character, remove a character, or replace a character.
    /// Given two strings, write a function to check if they are one edits (or zero edits) away.
    ///
    /// Solution:
    /// * two pointers
    /// </summary>
    internal class Task1_5OneAway
    {
        public bool IsOneAway(string first, string second)
        {
            if (Math.Abs(first.Length - second.Length) > 1)
            {
                return false;
            }

            var firstPointer = 0;
            var secondPointer = 0;
            var changes = 0;

            while (firstPointer != first.Length && secondPointer != second.Length)
            {
                if (first[firstPointer] != second[secondPointer])
                {
                    changes++;

                    if (changes > 1)
                        return false;

                    if (first.Length > second.Length)
                    {
                        firstPointer++;
                    }
                    else if (first.Length < second.Length)
                    {
                        secondPointer++;
                    }
                    else
                    {
                        firstPointer++;
                        secondPointer++;
                    }
                }
                else
                {
                    firstPointer++;
                    secondPointer++;
                }
            }

            return true;
        }
    }

    [TestFixture]
    public class Task1_5OneAwayTests
    {
        private static object[] testCases =
        {
            new object[] { "pale", "ple", true },
            new object[] { "pales", "pale", true },
            new object[] { "pale", "bale", true },
            new object[] { "pale", "bake", false },
            new object[] { "pale", "bales", false },
            new object[] { "bake", "bakesale", false },
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationHashMapTest(string first, string second, bool expectedResult)
        {
            // arrange
            var sut = new Task1_5OneAway();

            // act
            var result = sut.IsOneAway(first, second);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
