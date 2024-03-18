using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Implement pow(x, n), which calculates x raised to the power n (i.e., x^n).
    /// -100.0 < x < 100.0
    /// -2^31 <= n <= 2^31-1
    /// -10^4 <= x^n <= 10^4

    /// </summary>
    internal class Math_Pow_x_n
    {
        // Divide & Conquer paradigm: separate into sub-problems and solve them recursively
        // O(logn) time complexity -> log_2(10) = 3.32. 2^3.22 = 10. 10 can be squared 3.22 times.
        public double MyPow(double x, int n)
        {
            var result = MyPowInner(x, Math.Abs((long)n));

            return n < 0 ? 1 / result : result;
        }

        private double MyPowInner(double x, long n)
        {
            // 0^0 = 1
            if (n == 0)
            {
                return 1;
            }

            // 0^anything else = 0
            if (x == 0)
            {
                return 0;
            }

            var result = MyPowInner(x, n / 2);
            result *= result;

            return n % 2 == 1 ? x * result : result;
        }

        /// <summary>
        /// O(n) time, O(1) space
        /// </summary>
        public double MyPow_Naive(double x, int n)
        {
            var isExponentNegative = n < 0;
            n = Math.Abs(n);

            var result = 1.0;
            for (var i = 1; i <= n; i++)
            {
                result *= x;
            }

            return isExponentNegative ? 1 / result : result;
        }

        public static void TestCase()
        {
            var math = new Math_Pow_x_n();
            var shouldBe1024 = math.MyPow(2.0, 10);
            var shouldBe9_261 = math.MyPow(2.1, 3);
            var shouldBe0_25 = math.MyPow(2.0, -2);
            var shouldBeNotOverflow = math.MyPow(1.0, -2147483648);
        }
    }
}
