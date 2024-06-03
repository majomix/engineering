using NUnit.Framework;
using FluentAssertions;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Given two strings, write a method to decide if one is a permutation of the other.
    ///
    /// Questions to ask:
    /// Shall the permutation comparison be case-sensitive? e.g. is "God" a permutation of "dog"?
    /// Is white space significant? e.g. is "god     " different from "dog"?
    /// 
    /// Discussion:
    /// Permutation means both strings provide the same histogram of characters.
    ///
    /// Solution:
    /// * dictionary
    /// * sorting strings
    /// * count vector (limited charset) -> first count up, then count down, if same length then count vector must be all zeros
    /// </summary>
    internal class Task1_2CheckPermutation
    {
        public bool IsPermutationDictionary(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            var hashMapFirst = new Dictionary<char, int>();
            var hashMapSecond = new Dictionary<char, int>();

            foreach (char c in first)
            {
                hashMapFirst.TryAdd(c, 0);
                hashMapFirst[c]++;
            }

            foreach (var c in second)
            {
                hashMapSecond.TryAdd(c, 0);
                hashMapSecond[c]++;
            }

            foreach (var key in hashMapFirst.Keys)
            {
                if (!hashMapSecond.ContainsKey(key))
                    return false;

                if (hashMapFirst[key] != hashMapSecond[key])
                    return false;

                hashMapSecond.Remove(key);
            }

            return hashMapSecond.Count == 0;
        }

        public bool IsPermutationSorting(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            var firstOrdered = first
                .OrderBy(c => c)
                .ToArray();
            var secondOrdered = second
                .OrderBy(c => c)
                .ToArray();

            return new string(firstOrdered) == new string(secondOrdered);
        }

        public bool IsPermutationCountVector(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            // expects extended ASCII only!
            var availableCharactersInCharset = 256;
            var countVector = new int[availableCharactersInCharset];

            foreach (var ch in first)
            {
                if (ch > 255)
                    throw new ArgumentException("Unexpected character range!");

                countVector[ch]++;
            }

            foreach (var ch in second)
            {
                if (ch > 255)
                    throw new ArgumentException("Unexpected character range!");

                countVector[ch]--;

                if (countVector[ch] < 0)
                    return false;
            }

            return true;
        }
    }

    [TestFixture]
    public class Task1_2CheckPermutationTests
    {
        private static object[] testCases =
        {
            new object[] { "aaaa", "aaaa", true },
            new object[] { "abcd", "abcd", true },
            new object[] { "abcdea", "dcbeaa", true },
            new object[] { "qqqqwe", "weqqqq", true },
            new object[] { "qqqqwe", "wuqqq", false },
            new object[] { "qwertz", "qwertw", false },
            new object[] { "awewew", "wewewa", true}
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationHashMapTest(string first, string second, bool expectedResult)
        {
            // arrange
            var sut = new Task1_2CheckPermutation();

            // act
            var result = sut.IsPermutationDictionary(first, second);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationSortingTest(string first, string second, bool expectedResult)
        {
            // arrange
            var sut = new Task1_2CheckPermutation();

            // act
            var result = sut.IsPermutationSorting(first, second);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsPermutationCountVectorTest(string first, string second, bool expectedResult)
        {
            // arrange
            var sut = new Task1_2CheckPermutation();

            // act
            var result = sut.IsPermutationCountVector(first, second);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
