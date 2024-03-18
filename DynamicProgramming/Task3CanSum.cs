using System.Collections.Generic;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function canSum(targetSum, numbers) that takes in a targetSum and an array of numbers.
    /// The function should return a boolean indicating whether or not it is possible to generate targetSum using the numbers.
    /// You may use an element of the array as many times as needed.
    /// You may assume that all input numbers are non-negative.
    /// </summary>
    public class Task3CanSum
    {
        private Dictionary<int, bool> _memoization = new Dictionary<int, bool>();

        /// <summary>
        /// O(m*n) time, O(m) space
        /// </summary>
        public bool CanSumTabulation(int targetSum, List<int> numbers)
        {
            var table = new bool[targetSum + 1];
            table[0] = true;

            for (int i = 0; i <= targetSum; i++)
            {
                foreach (var number in numbers)
                {
                    if (table[i] && i + number < targetSum + 1)
                    {
                        table[i + number] = true;
                    }
                }
            }

            return table[targetSum];
        }


        /// <summary>
        /// time O(m*n), space O(m)
        /// </summary>
        public bool CanSumMemoization(int targetSum, List<int> numbers)
        {
            if (_memoization.ContainsKey(targetSum))
            {
                return _memoization[targetSum];
            }

            if (targetSum == 0)
                return true;

            if (targetSum < 0)
                return false;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                if (CanSumMemoization(remainder, numbers))
                {
                    _memoization[targetSum] = true;
                    return true;
                }
            }

            _memoization[targetSum] = false;
            return false;
        }

        /// <summary>
        /// Height of the tree is the worst case scenario. If sum is 300 and numbers contain 1, this would take 300x1
        /// m is the worst-case height, n is the amount of nodes on every level m
        /// time complexity is O(n^m), space is O(m)
        /// </summary>
        public bool CanSumNaive(int targetSum, List<int> numbers)
        {
            if (targetSum == 0)
                return true;

            if (targetSum < 0)
                return false;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                if (CanSumNaive(remainder, numbers))
                {
                    return true;
                }
            }

            return false;
        }

        public static void TestCase()
        {
            var canSum = new Task3CanSum();
            var shouldBeTrue = canSum.CanSumNaive(8, new List<int> { 5, 1, 3 });
            var shouldBeFalse = canSum.CanSumMemoization(300, new List<int> { 7, 14 });
            var shouldBeFalseTab = canSum.CanSumTabulation(300, new List<int> { 7, 14 });
            var shouldBeTrueTab = canSum.CanSumTabulation(8, new List<int> { 5, 1, 3 });
        }
    }
}
