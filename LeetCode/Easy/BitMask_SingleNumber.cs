namespace LeetCode.Easy
{
    /// <summary>
    /// Given a non-empty array of integers nums, every element appears twice except for one. Find that single one.
    /// You must implement a solution with a linear runtime complexity and use only constant extra space.
    /// </summary>
    public class BitMask_SingleNumber
    {
        public int SingleNumber(int[] nums)
        {
            int result = 0;

            foreach (var num in nums)
            {
                result ^= num;
            }

            return result;
        }

        public static void TestCase()
        {
            var bm = new BitMask_SingleNumber();
            var shouldBe5 = bm.SingleNumber(new int[] { 2, 2, 5 });
            var shouldBe4 = bm.SingleNumber(new int[] { 4, 1, 2, 1, 2 });
            var shouldBe1 = bm.SingleNumber(new int[] { 1 });
        }
    }
}
