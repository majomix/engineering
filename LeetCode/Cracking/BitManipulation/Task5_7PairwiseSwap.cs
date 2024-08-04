using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// Write a program to swap odd and even bits in an integer with as few instructions as possible.
    ///
    /// Solution:
    /// * mask all odd bits and shift them left, mask all even bits and shift them right, OR them together
    /// </summary>
    internal class Task5_7PairwiseSwap
    {
        public int SwapEvenAndOddBits(int number)
        {
            return ((number & 0b101010101010101010101010101010) >> 1) | ((number & 0b010101010101010101010101010101) << 1);
        }
    }

    [TestFixture]
    internal class Task5_7PairwiseSwapTests
    {
        private static object[] testCases =
        {
            new object[] { 1, 2 }
        };

        [TestCaseSource(nameof(testCases))]
        public void GetRequiredBitSwapsTest(int number, int expectedResult)
        {
            // arrange
            var sut = new Task5_7PairwiseSwap();

            // act
            var result = sut.SwapEvenAndOddBits(number);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
