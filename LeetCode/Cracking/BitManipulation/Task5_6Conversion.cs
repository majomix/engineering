using NUnit.Framework;
using FluentAssertions;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// Write a function to determine the number of bits you would need to flip to convert integer A to integer B.
    ///
    /// Conclusion:
    /// true if n is power of 2
    /// </summary>
    internal class Task5_6Conversion
    {
        public int GetRequiredBitSwaps(int a, int b)
        {
            var requiredSwaps = 0;

            var differentBits = a ^ b;

            while (differentBits != 0)
            {
                requiredSwaps += differentBits & 1; 
                differentBits >>= 1;
            }

            return requiredSwaps;
        }
    }

    [TestFixture]
    internal class Task5_6ConversionTests
    {
        private static object[] testCases =
        {
            new object[] { 1, 2, 2 },
            new object[] { 29, 15, 2 }
        };

        [TestCaseSource(nameof(testCases))]
        public void GetRequiredBitSwapsTest(int a, int b, int expectedResult)
        {
            // arrange
            var sut = new Task5_6Conversion();

            // act
            var result = sut.GetRequiredBitSwaps(a, b);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
