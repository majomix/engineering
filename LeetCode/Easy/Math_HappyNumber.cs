using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Easy
{
    /// <summary>
    /// Write an algorithm to determine if a number n is happy. A happy number is a number defined by the following process:
    /// 1. Starting with any positive integer, replace the number by the sum of the squares of its digits.
    /// 2. Repeat the process until the number equals 1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
    /// 3. Those numbers for which this process ends in 1 are happy.
    /// 
    /// Return true if n is a happy number, and false if not.
    /// 1 <= n <= 2^31 - 1
    /// </summary>
    internal class Math_HappyNumber
    {
        private HashSet<int> memo = new();

        public bool IsHappy(int n)
        {
            if (n == 1)
            {
                return true;
            }

            if (memo.Contains(n))
            {
                return false;
            }

            memo.Add(n);

            var digits = new List<int>();

            while (n > 0)
            {
                var digit = n % 10;
                digits.Add(digit);
                n /= 10;
            }

            var result = 0;
            foreach (var digit in digits)
            {
                result += digit * digit;
            }

            return IsHappy(result);
        }

        public static void TestCase()
        {
            var math = new Math_HappyNumber();
            var shouldBeTrue = math.IsHappy(19);
            var shouldBeFalse = math.IsHappy(2);
        }
    }
}
