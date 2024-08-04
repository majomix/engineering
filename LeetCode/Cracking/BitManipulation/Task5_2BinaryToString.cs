using FluentAssertions;
using NUnit.Framework;
using System.Text;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// Given a real number between 0 and 1 that is passed in as a double, print the binary representation.
    /// If the number cannot be represented accurately in binary with at most 32 characters, print "ERROR".
    ///
    /// Discussion:
    /// We're only interested in printing numbers in interval (0,1) rather than doing the binary of the actual IEEE 754.
    ///
    /// Let's assume following representations:
    /// 0.5 = 1b
    /// 0.25 = 01b
    /// 0.125 = 001b
    /// 0.75 = 11b
    /// </summary>
    internal class Task5_2BinaryToString
    {
        public string PrintBinary(double number)
        {
            var error = "ERROR";
            var stringBuilder = new StringBuilder();
            var threshold = 0.5;

            if (number >= 1 || number <= 0)
            {
                return error;
            }

            while (number > 0)
            {
                if (stringBuilder.Length > 32)
                    return error;

                if (number >= threshold)
                {
                    stringBuilder.Append("1");
                    number -= threshold;
                }
                else
                {
                    stringBuilder.Append("0");
                }

                threshold /= 2;
            }

            return stringBuilder.ToString();
        }
    }

    [TestFixture]
    internal class Task5_2BinaryToStringTests
    {
        private static object[] testCases =
        {
            new object[] { 0.5, "1" },
            new object[] { 0.25, "01" },
            new object[] { 0.125, "001" },
            new object[] { 0.75, "11" },
            new object[] { 1, "ERROR" },
            new object[] { 0, "ERROR" }
        };

        [TestCaseSource(nameof(testCases))]
        public void PrintBinaryTest(double number, string expectedResult)
        {
            // arrange
            var sut = new Task5_2BinaryToString();

            // act
            var result = sut.PrintBinary(number);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
