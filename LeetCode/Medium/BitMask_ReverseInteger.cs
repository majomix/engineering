using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given a signed 32-bit integer x, return x with its digits reversed. If reversing x causes the value to go outside the signed 32-bit integer range [-2^31, 2^31 - 1], then return 0.
    /// Assume the environment does not allow you to store 64-bit integers(signed or unsigned).
    /// -2^31 <= x <= 2^31 - 1
    /// </summary>
    internal class BitMask_ReverseInteger
    {
        public int Reverse(int x)
        {
            var maximumLastDigit = Int32.MaxValue % 10;
            var minimumLastDigit = Int32.MinValue % 10;

            var maximumDividedByTen = Int32.MaxValue / 10;
            var minimumDividedByTen = Int32.MinValue / 10;
            var result = 0;
            while (x != 0)
            {
                var lastDigit = x % 10;

                // check for overflow
                // if adding another digit would overflow
                if (result > maximumDividedByTen ||
                    // if adding another digit would be still OK but the digit itself would cause overflow
                    (result == maximumDividedByTen && lastDigit > maximumLastDigit))
                    return 0;

                // check for underflow
                if (result < minimumDividedByTen || (result == minimumDividedByTen && lastDigit < minimumLastDigit))
                    return 0;

                x /= 10;
                result *= 10;
                result += lastDigit;
            }

            return result;
        }

        public static void TestCase()
        {
            var bm = new BitMask_ReverseInteger();
            var shouldBe_321 = bm.Reverse(123);
            var shouldBe_neg321 = bm.Reverse(-123);
            var shouldBe_neg4321 = bm.Reverse(-1234);
            var shouldBe21 = bm.Reverse(120);
        }
    }
}
