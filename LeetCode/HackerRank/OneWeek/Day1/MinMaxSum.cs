using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Week1
{
    public static class MinMaxSum
    {
        /// <summary>
        /// PASSES 15/15
        /// 
        /// Given five positive integers, find the minimum and maximum values that can be calculated by summing exactly four of the five integers.
        /// Then print the respective minimum and maximum values as a single line of two space-separated long integers.
        /// </summary>
        public static void miniMaxSum(List<int> arr)
        {
            long sum = 0;
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (var number in arr)
            {
                if (min > number)
                {
                    min = number;
                }

                if (max < number)
                {
                    max = number;
                }
                sum += number;
            }

            Console.WriteLine($"{sum - max} {sum - min}");
        }
    }
}
