namespace LeetCode.Easy
{
    /// <summary>
    /// Given an array nums containing n distinct numbers in the range [0, n], return the only number in the range that is missing from the array.
    /// n == nums.length
    /// 1 <= n <= 10^4
    /// 0 <= nums[i] <= n
    /// All the numbers of nums are unique.
    /// Could you implement a solution using only O(1) extra space complexity and O(n) runtime complexity?
    /// </summary>
    public class BitMask_MissingNumber
    {
        public int MissingNumber(int[] nums)
        {
            var expectedCount = nums.Length + 1; // one number is missing
            var largestNumber = nums.Length; // values in array are zero-based

            var expectedSum = expectedCount * largestNumber / 2; // sum of arithmetic sequence
            var actualSum = 0;
            foreach (var number in nums)
            {
                actualSum += number;
            }

            return expectedSum - actualSum;
        }

        public static void TestCase()
        {
            var bm = new BitMask_MissingNumber();
            var shouldBe2 = bm.MissingNumber(new int[] { 3, 0, 1 });
            var shouldBe2Too = bm.MissingNumber(new int[] { 0, 1 });
            var shouldBe8 = bm.MissingNumber(new int[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 });
        }
    }
}
