using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.ArraysAndStrings
{
    /// <summary>
    /// Given a string, write a function to check if it is a permutation of a palindrome.
    ///
    /// Questions to ask:
    /// Can I ignore white space? Yes
    /// Is it case-sensitive? No
    ///
    /// Discussion:
    /// Palindrome can be checked by checking if the letters at the start and end are the same, then moving both pointers inwards.
    /// For palindrome permutation, only the histogram is relevant.
    /// 
    /// Solution:
    /// * histogram
    /// * bit vector (given restrictions of alphabet) -> memory effective store evens as 0 and odds as 1
    /// </summary>
    internal class Task1_4PalindromePermutation
    {
        public bool IsPalindromePermutation(string input)
        {
            var histogram = new Dictionary<char, int>();

            foreach (var c in input)
            {
                if (c == ' ')
                    continue;

                char value = c >= 'A' && c <= 'Z' ? (char)(c + ' ') : c;
                
                histogram.TryAdd(value, 0);
                histogram[value]++;
            }
            
            var numberOfOddCounts = 0;
            foreach (var pair in histogram)
            {
                if (pair.Value % 2 == 1)
                {
                    numberOfOddCounts++;
                }
            }

            return numberOfOddCounts <= 1;
        }
    }

    [TestFixture]
    public class Task1_4PalindromePermutationTests
    {
        private static object[] testCases =
        {
            new object[] { "Tact Coa", true }
        };

        [TestCaseSource(nameof(testCases))]
        public void IsPalindromePermutationTest(string input, bool expectedResult)
        {
            // arrange
            var sut = new Task1_4PalindromePermutation();

            // act
            var result = sut.IsPalindromePermutation(input);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
