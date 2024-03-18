using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Easy
{
    public class BitMask_ReverseBits
    {
        public uint reverseBits(uint n)
        {
            var result = 0u;

            for (var i = 0; i < 32; i++)
            {
                var bitToSet = n & 1;
                var offset = 32 - i - 1;
                result |= (bitToSet << offset);
                n >>= 1;
            }

            return result;
        }

        public static void TestCase()
        {
            var bm = new BitMask_ReverseBits();
            var shouldBe964176192 = bm.reverseBits(0b00000010100101000001111010011100);
            var shouldBe3221225471 = bm.reverseBits(0b11111111111111111111111111111101);
        }
    }
}
