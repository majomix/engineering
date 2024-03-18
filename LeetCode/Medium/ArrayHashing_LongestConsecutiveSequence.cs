using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence.
    /// You must write an algorithm that runs in O(n) time.
    /// 
    /// 0 <= nums.length <= 10^5
    /// -10^9 <= nums[i] <= 10^9

    /// </summary>
    internal class ArrayHashing_LongestConsecutiveSequence
    {
        public int LongestConsecutive(int[] nums)
        {
            var set = nums.ToHashSet();

            var longestSequence = 0;
            foreach (var number in nums)
            {
                if (set.Contains(number - 1))
                    continue;

                var currentSequence = 1;

                var forwardKey = number + 1;
                while (set.Contains(forwardKey))
                {
                    currentSequence++;
                }
                
                if (currentSequence > longestSequence)
                {
                    longestSequence = currentSequence;
                }
            }

            return longestSequence;
        }

        public int LongestConsecutive_Dictionary_TwoWaySearch(int[] nums)
        {
            var ds = new Dictionary<int, bool>();
            
            foreach (var number in nums)
            {
                ds[number] = false;
            }

            var longestSequence = 0;
            foreach (var number in nums)
            {
                if (ds[number])
                    continue;

                var currentSequence = 1;

                var forwardKey = number + 1;
                while (ds.ContainsKey(forwardKey))
                {
                    currentSequence++;
                    ds[forwardKey++] = true;
                }

                var backwardKey = number - 1;
                while (ds.ContainsKey(backwardKey))
                {
                    currentSequence++;
                    ds[backwardKey--] = true;
                }

                ds[number] = true;

                if (currentSequence > longestSequence)
                {
                    longestSequence = currentSequence;
                }
            }

            return longestSequence;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_LongestConsecutiveSequence();
            var shouldBe4 = arrhash.LongestConsecutive(new int[] { 100, 4, 200, 1, 3, 2 });
            var shouldBe9 = arrhash.LongestConsecutive(new int[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 });
        }
    }
}
