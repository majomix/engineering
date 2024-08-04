using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// Given a positive integer, print the next smallest and the next largest number that have the same amount of 1 bits in their binary representation.
    ///
    /// Discussion:
    /// Step 1: To increase, swap right-most zero in a non-trailing zero sequence with one.
    /// Step 2: Count 0s and 1s on the right-hand side of this swap and order them in groups of 0,1.
    /// Remark: Non-solvable if there is only trailing zero sequence.
    ///
    /// Step 1: To decrease, swap right-most one in a non-trailing one sequence with zero.
    /// Step 2: Count 0s and 1s on the right-hand side of this swap and order them in groups of 1,0.
    /// Remark: Non-solvable if there is only trailing one sequence.
    /// </summary>
    internal class NextNumberResult
    {
        public uint NextSmallest;
        public uint NextLargest;
    }
    
    internal class Task5_4NextNumber
    {
        public NextNumberResult FindNextNumbers(uint number)
        {
            var nextSmallest = GetPrevious(number);
            var nextLargest = GetNext(number);

            return new NextNumberResult { NextSmallest = nextSmallest, NextLargest = nextLargest };
        }

        private uint GetPrevious(uint number)
        {
            var zerosCount = 0;
            var onesCount = 0;

            var copy = number;
            while (copy > 0)
            {
                if ((copy & 1) == 0)
                {
                    zerosCount++;
                }
                else
                {
                    onesCount++;
                    
                    // break at right-most non-trailing one
                    if (zerosCount != 0)
                    {
                        break;
                    }
                }

                copy >>= 1;
            }

            var swapPosition = zerosCount + onesCount;

            uint clearMask = unchecked((uint)~0) << swapPosition;
            number &= clearMask;

            var moveSetMaskBy = swapPosition - onesCount - 1;
            var onesMask = (1 << onesCount) - 1;
            uint setMask = (uint)(onesMask << moveSetMaskBy);
            number |= setMask;

            return number;
        }

        private uint GetNext(uint number)
        {
            var zerosCount = 0;
            var onesCount = 0;

            var copy = number;
            while (copy > 0)
            {
                if ((copy & 1) == 0)
                {
                    zerosCount++;

                    // break at right-most non-trailing zero
                    if (onesCount != 0)
                    {
                        break;
                    }
                }
                else
                {
                    onesCount++;
                }

                copy >>= 1;
            }

            var swapPosition = zerosCount + onesCount;

            uint clearMask = unchecked((uint)~0) << swapPosition;
            number &= clearMask;

            uint firstSetMask = (uint)(1 << swapPosition - 1);
            uint secondSetMask = (uint)(1 << (onesCount - 1)) - 1;
            number |= firstSetMask;
            number |= secondSetMask;

            return number;
        }
    }

    [TestFixture]
    internal class Task5_4NextNumberTests
    {
        private static object[] testCases =
        {
            new object[] { (uint)20, new NextNumberResult { NextLargest = 24, NextSmallest = 18 } },
            new object[] { (uint)35, new NextNumberResult { NextLargest = 37, NextSmallest = 28 } },
            new object[] { (uint)13948, new NextNumberResult { NextLargest = 13967, NextSmallest = 13946 } }
        };

        [TestCaseSource(nameof(testCases))]
        public void PrintBinaryTest(uint number, NextNumberResult expectedResult)
        {
            // arrange
            var sut = new Task5_4NextNumber();

            // act0

            var result = sut.FindNextNumbers(number);

            // assert
            result.NextLargest.Should().Be(expectedResult.NextLargest);
            result.NextSmallest.Should().Be(expectedResult.NextSmallest);
        }
    }
}
