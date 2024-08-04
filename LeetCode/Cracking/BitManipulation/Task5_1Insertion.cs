using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// You are given two 32-bit numbers n and m, and two bit positions i and j.
    ///
    /// Write a method to insert M into N such that M starts at bit j and ends at bit i.
    ///
    /// You can assume that the bits j through i have enough space to fit all of m.
    ///
    /// Example:
    /// Input ->  n = 10000000000, m = 10011, i = 2, j = 6
    /// Output -> n = 10001001100
    /// </summary>
    internal class Task5_1Insertion
    {
        public int UpdateBits(int n, int m, int i, int j)
        {
            var mask = ~0 << 1;
            for (var k = 0; k < j; k++)
            {
                mask <<= 1;

                if (k >= i)
                {
                    n &= mask;
                }
            }

            m <<= i;
            n |= m;

            return n;
        }
    }

    [TestFixture]
    internal class Task5_1InsertionTests
    {
        [Test]
        public void UpdateBitsTest()
        {
            // arrange
            var sut = new Task5_1Insertion();

            // act
            var result = sut.UpdateBits(0b10000000000, 0b10011, 2, 6);

            // assert
            result.Should().Be(0b10001001100);
        }
    }
}
