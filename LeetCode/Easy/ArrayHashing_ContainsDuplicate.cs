namespace LeetCode.Easy
{
    /// <summary>
    /// Given an integer array nums, return true if any value appears at least twice in the array, and return false if every element is distinct.
    /// </summary>
    public class ArrayHashing_ContainsDuplicate
    {
        /// <summary>
        /// Brute force with double loop: O(n^2) time, O(1) space
        /// Sorting: O(n*logn) time, O(1) space
        /// HashSet: O(n) time, O(n) space
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool ContainsDuplicate(int[] nums)
        {
            var hashSet = new HashSet<int>();

            foreach (var number in nums)
            {
                if (hashSet.Contains(number))
                {
                    return true;
                }

                hashSet.Add(number);
            }

            return false;
        }

        public static void TestCase()
        {
            var arrhash = new ArrayHashing_ContainsDuplicate();
            var shouldBeTrue = arrhash.ContainsDuplicate(new int[] { 1, 2, 3, 1 });
            var shouldBeFalse = arrhash.ContainsDuplicate(new int[] { 1, 2, 3, 4 });
            var shouldBeTrue2 = arrhash.ContainsDuplicate(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 });
        }
    }
}
