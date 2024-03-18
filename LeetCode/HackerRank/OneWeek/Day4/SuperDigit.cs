using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day4
{
    internal class SuperDigit
    {
        /// <summary>
        /// We define super digit of an integer using the following rules:
        /// Given an integer, we need to find the super digit of the integer.
        /// If has only digit, then its super digit is x.
        /// Otherwise, the super digit of is equal to the super digit of the sum of the digits of x.
        /// The number p is created by concatenating n k the string times.
        /// </summary>
        /// <returns></returns>
        public static int superDigit(string n, int k)
        {
            long sum = 0;

            foreach (var letter in n)
            {
                sum += letter - '0';
            }

            sum *= k;

            return SuperDigitInternal(sum);
        }

        private static int SuperDigitInternal(long value)
        {
            int newSum = 0;

            while (value > 0)
            {
                newSum += (int)(value % 10);
                value /= 10;
            }

            if (newSum > 9)
            {
                return SuperDigitInternal(newSum);
            }

            return newSum;
        }

        public static void TestCase()
        {
            var shouldBe2 = superDigit("9875", 1);
            var shouldBe8 = superDigit("9875", 4);
        }
    }
}
