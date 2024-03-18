using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Easy
{
    /// <summary>
    /// You are given a large integer represented as an integer array digits, where each digits[i] is the ith digit of the integer.
    /// The digits are ordered from most significant to least significant in left-to-right order. The large integer does not contain any leading 0's.
    /// Increment the large integer by one and return the resulting array of digits.
    /// 
    /// 1 <= digits.length <= 100
    /// 0 <= digits[i] <= 9
    /// digits does not contain any leading 0's.

    /// </summary>
    internal class Math_PlusOne
    {
        public int[] PlusOne(int[] digits)
        {
            var list = new List<int>();

            var currentDigit = digits[digits.Length - 1] + 1;
            var carry = false;

            for (var i = digits.Length - 1; i >= 0; i--)
            {
                if (i != digits.Length - 1)
                {
                    currentDigit = digits[i];
                }

                if (carry)
                {
                    currentDigit++;
                }

                carry = currentDigit > 9;
                list.Add(currentDigit % 10);
            }

            if (carry)
            {
                list.Add(1);
            }

            var result = new int[list.Count];
            var pos = 0;
            foreach (var digit in list)
            {
                result[list.Count - 1 - pos++] = digit;
            }

            return result;
        }

        public static void TestCase()
        {
            var math = new Math_PlusOne();
            var shouldBe1_2_4 = math.PlusOne(new int[] { 1, 2, 3 });
            var shouldBe4_3_2_1 = math.PlusOne(new int[] { 4, 3, 2, 1 });
            var shouldBe1_0 = math.PlusOne(new int[] { 9 });
         }
    }
}
