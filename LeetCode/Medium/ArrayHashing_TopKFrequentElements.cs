using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given an integer array nums and an integer k, return the k most frequent elements. You may return the answer in any order.
    /// 
    /// Constraints:
    /// 1 <= nums.length <= 10^5
    /// -10^4 <= nums[i] <= 10^4
    /// k is in the range[1, the number of unique elements in the array].
    /// It is guaranteed that the answer is unique.
    /// </summary>
    internal class ArrayHashing_TopKFrequentElements
    {
        // bucketting
        // time complexity O(n)
        public int[] TopKFrequent(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (!dict.ContainsKey(num))
                {
                    dict[num] = 0;
                }
                dict[num]++;
            }

            var frequencies = new List<List<int>>();
            for (var i = 0; i < nums.Length + 1; i++)
            {
                frequencies.Add(new List<int>());
            }

            foreach (var keyValuePair in dict)
            {
                frequencies[keyValuePair.Value].Add(keyValuePair.Key);
            }

            var result = new int[k];

            var resultIndex = 0;
            for (var i = nums.Length; i >= 0; i--)
            {
                foreach (var value in frequencies[i])
                {
                    result[resultIndex++] = value;
                    if (resultIndex == k)
                    {
                        return result;
                    }
                }

            }

            return null;
        }

        /// <summary>
        /// time complexity O(n*logn)
        /// </summary>
        public int[] TopKFrequent_PriorityQueue(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (!dict.ContainsKey(num))
                {
                    dict[num] = 0;
                }
                dict[num]++;
            }

            var frequencies = new PriorityQueue<int, int>(nums.Length);
            foreach (var keyValuePair in dict)
            {
                // priority queue uses min-heap so we need to invert priorities
                frequencies.Enqueue(keyValuePair.Key, -keyValuePair.Value); // n*logn
            }

            var result = new int[k];

            for (var i = 0; i < k; i++)
            {
                result[i] = frequencies.Dequeue(); // n*logn
            }

            return result;
        }

        // slow
        public int[] TopKFrequent_LINQ(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (!dict.ContainsKey(num))
                {
                    dict[num] = 0;
                }
                dict[num]++;
            }

            return dict.OrderByDescending(g => g.Value).Take(k).Select(g => g.Key).ToArray();
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_TopKFrequentElements();
            var shouldBe1_2 = arrhash.TopKFrequent(new int[] { 1, 1, 1, 2, 2, 3 }, 2);
            var shouldBe1 = arrhash.TopKFrequent(new int[] { 1 }, 1);
            var shouldBeMinus1 = arrhash.TopKFrequent(new int[] { -1, -1 }, 1);
        }
    }
}
