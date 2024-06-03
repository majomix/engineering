using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Implement an algorithm to determine if a string has all unique characters.
    ///
    /// Questions to ask:
    /// What character set?
    /// 
    /// Solution:
    /// * bit vector
    /// * hashmap
    /// 
    /// What if you cannot use additional data structures?
    ///
    /// Solution:
    /// * two loops -> O(n^2)
    /// * in-place sort -> O(n*log(n))
    /// </summary>
    internal class Task1_1IsUnique
    {
        public bool IsUniqueHashSet(string input)
        {
            var hashMap = new HashSet<char>();

            foreach (var ch in input)
            {
                var added = hashMap.Add(ch);
                
                if (!added)
                    return false;
            }

            return true;
        }

        public bool IsUniqueBitVector(string input)
        {
            var availableCharactersInCharset = 256;

            // expects extended ASCII only!
            var bitVector = new bool[availableCharactersInCharset];

            // cannot form unique strings if input is longer than available characters
            if (input.Length > availableCharactersInCharset)
                return false;

            foreach (var ch in input)
            {
                if (ch > 255)
                    throw new ArgumentException("Unexpected character range!");

                if (bitVector[ch])
                    return false;

                bitVector[ch] = true;
            }

            return true;
        }
        
        public bool IsUniqueNoDataStructuresSlow(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = i + 1; j < input.Length; j++)
                {
                    if (input[i] == input[j])
                        return false;
                }
            }

            return true;
        }
    }

    [TestFixture]
    public class Task1_1IsUniqueTests
    {
        private static object[] testCases =
        {
            new object[] { "aaaa", false },
            new object[] { "abcd", true },
            new object[] { "abcdea", false },
            new object[] { "abcddea", false },
            new object[] { "qwertz", true },
            new object[] { "qqqqwe", false }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsUniqueHashSetTest(string input, bool expectedResult)
        {
            // arrange
            var sut = new Task1_1IsUnique();

            // act
            var result = sut.IsUniqueHashSet(input);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsUniqueBitVectorTest(string input, bool expectedResult)
        {
            // arrange
            var sut = new Task1_1IsUnique();

            // act
            var result = sut.IsUniqueBitVector(input);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void IsUniqueNoDataStructuresSlowTest(string input, bool expectedResult)
        {
            // arrange
            var sut = new Task1_1IsUnique();

            // act
            var result = sut.IsUniqueNoDataStructuresSlow(input);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
