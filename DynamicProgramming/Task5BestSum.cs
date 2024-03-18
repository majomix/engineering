using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming
{
    /// <summary>
    /// Write a function bestSum(targetSum, numbers) that takes in a targetSum and an array of numbers.
    /// The function should return an array containing the shortest combination of numbers that add up to exactly the targetSum.
    /// If there are many possible combinations, you may return any single one.
    /// </summary>
    public class Task5BestSum
    {
        private Dictionary<int, List<int>> _memoization = new Dictionary<int, List<int>>();

        /// <summary>
        /// O(m^2*n) time
        /// O(m^2) space
        /// </summary>
        public List<int> BestSumTabulation(int targetSum, List<int> numbers)
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
                            var currentCombination = table[i].ToList();
                            currentCombination.Add(number);
                            if (table[i + number] == null || table[i + number].Count > currentCombination.Count)
                            {
                                table[i + number] = currentCombination;
                            }
                        }
                    }
                }

            }

            return table[targetSum];
        }

        /// <summary>
        /// time: O(m^2 * n) - how many keys are there in the memo object, n comes from foreach and m comes from result.Add
        /// space: O(m^2)
        /// </summary>
        public List<int> BestSumMemoization(int targetSum, List<int> numbers)
        {
            if (_memoization.ContainsKey(targetSum))
            {
                return _memoization[targetSum];
            }

            if (targetSum == 0)
                return new List<int>();

            if (targetSum < 0)
                return null;

            List<int> shortestCombination = null;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                var result = BestSumMemoization(remainder, numbers);

                if (result != null)
                {
                    result = result.ToList();
                    result.Add(number);

                    if (shortestCombination == null || result.Count < shortestCombination.Count)
                    {
                        shortestCombination = result;
                    }
                }
            }

            _memoization[targetSum] = shortestCombination;

            return shortestCombination;
        }

        /// <summary>
        /// m = target sum
        /// n = numbers.Count
        /// time O(n^m * m) - * m comes from list.Add()
        /// space O(m^2)
        /// </summary>
        public List<int> BestSumNaive(int targetSum, List<int> numbers)
        {
            if (targetSum == 0)
                return new List<int>();

            if (targetSum < 0)
                return null;

            List<int> shortestCombination = null;

            foreach (var number in numbers)
            {
                var remainder = targetSum - number;
                var result = BestSumNaive(remainder, numbers);
                if (result != null)
                {
                    result.Add(number);
                    if (shortestCombination == null || shortestCombination.Count > result.Count)
                    {
                        shortestCombination = result;
                    }
                }
            }

            return shortestCombination;
        }

        public static void TestCase()
        {
            var bestSum = new Task5BestSum();
            var result1 = bestSum.BestSumNaive(7, new List<int> { 5, 3, 4, 7 });
            var result2 = bestSum.BestSumNaive(8, new List<int> { 2, 3, 5 });
            var result3 = bestSum.BestSumNaive(8, new List<int> { 1, 4, 5 });
            var result3Tab = bestSum.BestSumTabulation(8, new List<int> { 1, 4, 5 });
            var result4 = bestSum.BestSumMemoization(100, new List<int> { 1, 2, 5, 25 });
            var result4Tab = bestSum.BestSumTabulation(100, new List<int> { 1, 2, 5, 25 });
        }
    }
}
