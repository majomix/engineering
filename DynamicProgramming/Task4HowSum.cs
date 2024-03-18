using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function howSum(targetSum, numbers) that takes in a targetSum and an array of numbers.
    /// The function should return an array containing any combination of elements that add up to exactly the targetSum. If no such combination exists, return null.
    /// If there are many possible combinations, you may return any single one.
    /// </summary>
    public class Task4HowSum
    {
        private Dictionary<int, List<int>> _memoization = new Dictionary<int, List<int>>();

        /// <summary>
        /// O(m*n) time, O(m) space
        /// </summary>
        public List<int> HowSumTabulation(int targetSum, List<int> numbers)
        {
            var table = new List<int>[targetSum + 1];
            table[0] = new List<int>();

            for (int i = 0; i <= targetSum; i++)
            {
                if (table[i] != null)
                {
                    foreach (var number in numbers)
                    {
                        if (i + number < targetSum + 1)
                        {
                            table[i + number] = table[i].ToList();
                            table[i + number].Add(number);
                        }
                    }
                }

            }

            return table[targetSum];
        }

        /// <summary>
        /// time O(n*m^2)
        /// space O(m^2)
        /// </summary>
        public List<int> HowSumMemoization(int targetSum, List<int> numbers)
        {
            if (_memoization.ContainsKey(targetSum))
            {
                return _memoization[targetSum];
            }

            if (targetSum == 0)
                return new List<int>();

            if (targetSum < 0)
                return null;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                var result = HowSumMemoization(remainder, numbers);

                if (result != null)
                {
                    result.Add(number);
                    _memoization[targetSum] = result;
                    return result;
                }
            }

            _memoization[targetSum] = null;

            return null;
        }

        /// <summary>
        /// m = target sum
        /// n = numbers.Count
        /// time O(n^m * m) - * m comes from list.Add()
        /// space O(m)
        /// </summary>
        public List<int> HowSumNaive(int targetSum, List<int> numbers)
        {
            if (targetSum == 0)
                return new List<int>();

            if (targetSum < 0)
                return null;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                var result = HowSumNaive(remainder, numbers);

                if (result != null)
                {
                    result.Add(number);
                    return result;
                }
            }

            return null;
        }

        public static void TestCase()
        {
            var howSum = new Task4HowSum();
            var result1 = howSum.HowSumNaive(7, new List<int> { 2, 3 });
            var result2 = howSum.HowSumNaive(7, new List<int> { 5, 3, 4, 7 });
            var result2Tab = howSum.HowSumTabulation(7, new List<int> { 5, 3, 4, 7 });
            var result3 = howSum.HowSumNaive(8, new List<int> { 3, 5 });
            var result4 = howSum.HowSumMemoization(300, new List<int> { 7, 14 });
            var result4Tab = howSum.HowSumTabulation(300, new List<int> { 7, 14 });
        }
    }
}
