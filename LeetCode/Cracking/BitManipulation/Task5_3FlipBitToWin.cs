using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// You have an integer and you can flip exactly one bit from 0 to 1.
    ///
    /// Write code to find the length of the longest sequence of 1s you could create.
    /// </summary>
    internal class Task5_3FlipBitToWin
    {
        public int FindLongestSequenceOfOnesAfterFlip(uint number)
        {
            var previousSequence = 0;
            var currentSequence = 0;
            var longestSequence = 0;
            var hasZeroBetweenSequences = false;

            for (var i = 0; i < 32; i++)
            {
                if ((number & 1) == 1)
                {
                    currentSequence++;
                }
                else
                {
                    hasZeroBetweenSequences = true;
                    previousSequence = currentSequence;
                    currentSequence = 0;
                }

                // previous, current and +1 for the 0 in between if there was any
                var flippedSeparatorBetweenSequences = hasZeroBetweenSequences ? 1 : 0;
                var combinedSequenceLength = currentSequence + previousSequence + flippedSeparatorBetweenSequences;
                if (combinedSequenceLength > longestSequence)
                {
                    longestSequence = combinedSequenceLength;
                }
                number >>= 1;
            }

            return longestSequence;
        }
    }


    [TestFixture]
    internal class Task5_3FlipBitToWinTests
    {
        private static object[] testCases =
        {
            new object[] { (uint)0b0001_1111_0110_1110_0000_0000_0000_0000, 8 },
            new object[] { (uint)0b11111111111111111111111111111111, 32 },
            new object[] { (uint)0b01111111111111111111111111111111, 32 },
            new object[] { (uint)0b11111111111111111111111111111110, 32 },
            new object[] { (uint)0b11111111111110111111111111111111, 32 },
            new object[] { (uint)0, 1 },
            new object[] { (uint)1, 2 },
        };

        [TestCaseSource(nameof(testCases))]
        public void PrintBinaryTest(uint number, int expectedResult)
        {
            // arrange
            var sut = new Task5_3FlipBitToWin();

            // act
            var result = sut.FindLongestSequenceOfOnesAfterFlip(number);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
