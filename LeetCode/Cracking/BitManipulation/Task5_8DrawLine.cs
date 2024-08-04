using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.BitManipulation
{
    /// <summary>
    /// A monochrome screen is stored as a single array of bytes, allowing eight consecutive pixels to be stored in one byte.
    /// The screen has width divisible by 8. The height of the screen can be derived from the length of the array.
    ///
    /// Implement a function that draws a horizontal line from (x1, y) to (x2, y).
    /// </summary>
    internal class Task5_8DrawLine
    {
        public void DrawLine(byte[] screen, int width, int x1, int x2, int y)
        {
            if (x2 < x1 || x1 > width)
                return;
            
            var startRowIndex = width / 8 * y;

            var startByte = startRowIndex + x1 / 8;
            var bitStart = x1 - 8 * startByte;
            var endByte = startRowIndex + x2 / 8;
            var bitEnd = x2 - 8 * endByte;

            // special case
            if (startByte == endByte)
            {
                screen[startByte] = FillByteByBits(bitStart, bitEnd);
                return;
            }

            // start
            screen[startByte] = FillByteByBits(bitStart, 7);

            // middle
            var bytesBetweenStartEnd = endByte - startByte;
            for (var i = 1; i <= bytesBetweenStartEnd - 1; i++)
            {
                screen[startByte + i] = 0xFF;
            }

            // end
            screen[endByte] = FillByteByBits(0, bitEnd);
        }

        internal byte FillByteByBits(int bitStart, int bitEnd)
        {
            var onesSequenceLength = bitEnd - bitStart + 1;
            var onesMask = (1 << onesSequenceLength) - 1;
            var shiftMaskBy = 8 - onesSequenceLength - bitStart;
            var result = onesMask << shiftMaskBy;

            return (byte)result;
        }
    }

    [TestFixture]
    internal class Task5_8DrawLineTests
    {
        private static object[] testCasesByteGenerator =
        {
            new object[] { 0, 0, (byte)0b10000000 },
            new object[] { 0, 1, (byte)0b11000000 },
            new object[] { 7, 7, (byte)0b00000001 },
            new object[] { 6, 7, (byte)0b00000011 },
            new object[] { 0, 7, (byte)0b11111111 }
        };

        [TestCaseSource(nameof(testCasesByteGenerator))]
        public void FillByteByBits(int start, int end, byte expectedResult)
        {
            // arrange
            var sut = new Task5_8DrawLine();

            // act
            var result = sut.FillByteByBits(start, end);

            // assert
            result.Should().Be(expectedResult);
        }

        private static object[] testCases =
        {
            new object[] { 0, 1, 0, new byte[] { 0xC0, 0, 0, 0, 0, 0, 0, 0 } },
            new object[] { 1, 2, 0, new byte[] { 0x60, 0, 0, 0, 0, 0, 0, 0 } },
            new object[] { 1, 8, 0, new byte[] { 0x7F, 0x80, 0, 0, 0, 0, 0, 0 } },
            new object[] { 1, 16, 0, new byte[] { 0x7F, 0xFF, 0x80, 0, 0, 0, 0, 0 } },
            new object[] { 1, 24, 0, new byte[] { 0x7F, 0xFF, 0xFF, 0x80, 0, 0, 0, 0 } },
            new object[] { 0, 31, 0, new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0, 0, 0, 0 } },
            new object[] { 0, 1, 1, new byte[] { 0, 0, 0, 0, 0xC0, 0, 0, 0 } },
            new object[] { 1, 2, 1, new byte[] { 0, 0, 0, 0, 0x60, 0, 0, 0 } },
            new object[] { 1, 8, 1, new byte[] { 0, 0, 0, 0, 0x7F, 0x80, 0, 0 } },
            new object[] { 1, 16, 1, new byte[] { 0, 0, 0, 0, 0x7F, 0xFF, 0x80, 0 } },
            new object[] { 1, 24, 1, new byte[] { 0, 0, 0, 0, 0x7F, 0xFF, 0xFF, 0x80 } },
            new object[] { 0, 31, 1, new byte[] { 0, 0, 0, 0, 0xFF, 0xFF, 0xFF, 0xFF } }
        };

        [TestCaseSource(nameof(testCases))]
        public void DrawLine(int x1, int x2, int y, byte[] expectedResult)
        {
            // arrange
            var sut = new Task5_8DrawLine();

            // act
            var screen = new byte[8];
            sut.DrawLine(screen, 32, x1, x2, y);

            // assert
            screen.Should().BeEquivalentTo(expectedResult);
        }
    }
}
