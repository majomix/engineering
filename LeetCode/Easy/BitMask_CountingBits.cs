namespace LeetCode.Easy
{
    /// <summary>
    /// Given an integer n, return an array ans of length n + 1 such that for each i (0 <= i <= n), ans[i] is the number of 1's in the binary representation of i.
    /// 0 <= n <= 10^5
    /// </summary>
    public class BitMask_CountingBits
    {
        // n * logn
        public int[] CountBits(int n)
        {
            var result = new int[n + 1];

            for (var i = 0; i <= n; i++)
            {
                var ones = 0;
                var temp = i;
                for (var j = 0; j < 32; j++)
                {
                    ones += temp & 1;
                    temp >>= 1;
                }
                result[i] = ones;
            }

            return result;
        }

        public int[] CountBits_DP(int n)
        {
            var result = new int[n + 1];
            var offset = 1;

            for (var i = 1; i <= n; i++)
            {
                if (offset * 2 == i)
                {
                    offset = i;
                }

                result[i] = 1 + result[i - offset];
            }

            return result;
        }

        public static void TestCase()
        {
            var bm = new BitMask_CountingBits();
            var shouldBe5 = bm.CountBits_DP(2);
            var shouldBe4 = bm.CountBits_DP(5);
        }
    }
}
