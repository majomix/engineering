namespace LeetCode.Easy
{
    /// <summary>
    /// Write a function that takes an unsigned integer and returns the number of '1' bits it has (also known as the Hamming weight).
    /// </summary>
    public class BitMask_HammingWeight
    {
        public int HammingWeight(uint n)
        {
            var ones = 0;

            while (n > 0)
            {
                if ((n & 1) == 1)
                {
                    ones++;
                }

                n >>= 1;
            }

            return ones;
        }

        public int HammingWeight_MoreEfficient(uint n)
        {
            var ones = 0;

            while (n > 0)
            {
                n &= n - 1;
                ones++;
            }

            return ones;
        }

        public static void TestCase()
        {
            var bm = new BitMask_HammingWeight();
            var shouldBe3 = bm.HammingWeight_MoreEfficient(0b00000000000000000000000000001011);
            var shouldBe1 = bm.HammingWeight_MoreEfficient(0b00000000000000000000000010000000);
            var shouldBe31 = bm.HammingWeight(0b11111111111111111111111111111101);
        }
    }
}
