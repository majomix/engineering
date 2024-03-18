using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.HackerRank.OneWeek.Day2
{
    internal class CountingSort
    {
        /// <summary>
        /// PASSED 5/5
        /// 
        /// Another sorting method, the counting sort, does not require comparison. Instead, you create an integer array whose index range covers the entire range of values
        /// in your array to sort. Each time a value occurs in the original array, you increment the counter at that index. At the end, run through your counting array, printing
        /// the value of each non-zero valued index that number of times.
        /// </summary>
        public static List<int> countingSort(List<int> arr)
        {
            var output = new int[100];

            foreach (var number in arr)
            {
                output[number]++;
            }

            return output.ToList();
        }
    }
}
