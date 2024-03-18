using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Medium
{
    /// <summary>
    /// Given an integer array nums, return an array such that answer[i] is equal to the product of all the elements of nums except nums[i].
    /// The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.
    /// You must write an algorithm that runs in O(n) time and without using the division operation.
    /// Can you solve the problem in O(1) extra space complexity? (The output array does not count as extra space for space complexity analysis.)
    /// 
    /// 2 <= nums.length <= 10^5
    /// -30 <= nums[i] <= 30
    /// The product of any prefix or suffix of nums is guaranteed to fit in a 32-bit integer.
    /// </summary>
    internal class ArrayHashing_ProductOfArrayExceptSelf
    {
        // same as ProductExceptSelfWithExtraSpace just with reusing the result array
        public int[] ProductExceptSelf(int[] nums)
        {
            var result = new int[nums.Length];

            for (var i = 0; i < nums.Length; i++)
            {
                if (i == 0)
                {
                    result[i] = 1;
                    continue;
                }

                result[i] = nums[i - 1] * result[i - 1];
            }

            var initialBackwards = nums.Length - 1;
            var currentPostfix = nums[initialBackwards];
            for (var i = initialBackwards - 1; i >= 0; i--)
            {
                result[i] *= currentPostfix;
                currentPostfix *= nums[i];
            }

            return result;
        }

        // split the iteration for pre-values and post-values and multiply those
        public int[] ProductExceptSelfWithExtraSpace(int[] nums)
        {
            var result = new int[nums.Length];

            var prefix = new int[nums.Length];
            var postfix = new int[nums.Length];

            for (var i = 0; i < nums.Length; i++)
            {
                if (i == 0)
                {
                    prefix[i] = nums[i];
                    continue;
                }
                
                prefix[i] = nums[i] * prefix[i - 1];
            }

            var initialBackwards = nums.Length - 1;
            for (var i = initialBackwards; i >= 0; i--)
            {
                if (i == initialBackwards)
                {
                    postfix[i] = nums[i];
                    continue;
                }

                postfix[i] = nums[i] * postfix[i + 1];
            }

            for (var i = 0; i < nums.Length; i++)
            {
                var prefixValue = 1;
                var postfixValue = 1;

                if (i > 0)
                {
                    prefixValue = prefix[i - 1];
                }

                if (i < nums.Length - 1)
                {
                    postfixValue = postfix[i + 1];
                }

                result[i] = prefixValue * postfixValue;
            }

            return result;
        }

        public int[] ProductExceptSelfWithDivision(int[] nums)
        {
            var result = new int[nums.Length];

            var totalSum = 1;
            var containsZero = false;

            foreach (var num in nums)
            {
                if (num == 0)
                {
                    containsZero = true;
                }
                else
                {
                    totalSum *= num;
                }
            }

            for (var i = 0; i < nums.Length; i++)
            {
                if (containsZero && nums[i] != 0)
                {
                    result[i] = 0;
                }
                else if (nums[i] == 0)
                {
                    result[i] = totalSum;
                }
                else
                {
                    result[i] = totalSum / nums[i];
                }
            }

            return result;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_ProductOfArrayExceptSelf();
            var shouldBe_24_12_8_6 = arrhash.ProductExceptSelf(new int[] { 1, 2, 3, 4 });
            var shouldBe_0_0_9_0_0 = arrhash.ProductExceptSelf(new int[] { -1, 1, 0, -3, 3 });
        }
    }
}
