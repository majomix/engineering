namespace LeetCode.Easy
{
    /// <summary>
    /// Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target
    /// You may assume that each input would have exactly one solution, and you may not use the same element twice.
    /// You can return the answer in any order.
    /// 2 <= nums.length <= 10^4
    /// -10^9 <= nums[i] <= 10^9 -> 10^9 is int range
    /// -10^9 <= target <= 10^9
    /// </summary>
    public class ArrayHashing_TwoSum
    {
        // brute force: O(n^2) time, O(1) space
        // dictionary: O(n) time, O(n) space
        public int[] TwoSum(int[] nums, int target)
        {
            var memo = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                var difference = target - nums[i];

                if (memo.ContainsKey(difference))
                {
                    return new int[] { i, memo[difference] };
                }

                memo[nums[i]] = i;
            }

            return null;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_TwoSum();
            var shouldBe0_2 = arrhash.TwoSum(new int[] { 2, 11, 7, 15 }, 9);
            var shouldBe1_2 = arrhash.TwoSum(new int[] { 3, 2, 4 }, 6);
            var shouldBe0_1 = arrhash.TwoSum(new int[] { 3, 3 }, 6);
        }
    }
}
