using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given two integers a and b, return the sum of the two integers without using the operators + and -.
    /// -1000 <= a, b <= 1000
    /// </summary>
    public class BitMask_SumOfTwoNumbers
    {
        // 3: 0011
        // 6: 0110
        // -------
        // 9: 1001

        // 5: 0101
        // 8: 1000
        //13: 1101

        //-1: 1111
        // 1: 0001
        // 0: 0000

        //18: 10010
        // 2: 00010
        //20: 10100

        //20: 010100
        //30: 011110
        //of: 11100-
        //50: 110010

        // overflow arithmetic:
        // A B O
        // 0 0 0 -> 0
        // 0 0 1 -> 0
        // 0 1 0 -> 0
        // 0 1 1 -> 1
        // 1 0 0 -> 0
        // 1 0 1 -> 1
        // 1 1 0 -> 1
        // 1 1 1 -> 1
        public int GetSum(int a, int b)
        {
            var overflow = 0;
            var result = 0;
            for (var i = 0; i < 32; i++)
            {
                var targetBitA = a & 1;
                var targetBitB = b & 1;

                result |= (targetBitA ^ targetBitB ^ overflow) << i;
                
                overflow = targetBitA & targetBitB | targetBitA & overflow | targetBitB & overflow;
                a >>= 1;
                b >>= 1;
            }

            return result;
        }


        public static void TestCase()
        {
            var bm = new BitMask_SumOfTwoNumbers();
            var shouldBe13 = bm.GetSum(5, 8);
            var shouldBe20 = bm.GetSum(18, 2);
            var shouldBe50 = bm.GetSum(20, 30);
        }
    }
}
