using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Week1
{
    internal class PlusMinus
    {
        /// <summary>
        /// PASSES 15/15
        /// 
        /// Given an array of integers, calculate the ratios of its elements that are positive, negative, and zero. Print the decimal value of each fraction on a new line with
        /// places after the decimal. Note: This challenge introduces precision problems.The test cases are scaled to six decimal places, though answers with absolute error of
        /// up to are acceptable.
        /// Print the following lines, each to decimals:
        /// proportion of positive values
        /// proportion of negative values
        /// proportion of zeros
        /// </summary>
        public static void plusMinus(List<int> arr)
        {
            double totalItems = arr.Count;
            var numberOfNegative = 0;
            var numberOfPositive = 0;
            var numberOfZero = 0;

            foreach (var item in arr)
            {
                if (item == 0)
                {
                    numberOfZero++;
                }
                else if (item > 0)
                {
                    numberOfPositive++;
                }
                else
                {
                    numberOfNegative++;
                }
            }

            Console.WriteLine(numberOfPositive > 0 ? numberOfPositive / totalItems : 0);
            Console.WriteLine(numberOfNegative > 0 ? numberOfNegative / totalItems : 0);
            Console.WriteLine(numberOfZero > 0 ? numberOfZero / totalItems : 0);
        }
    }
}
