using System.Collections.Generic;

namespace DynamicProgramming
{
    /// <summary>
    /// You are a traveller on a 2D grid. You begin in the top-left corner and your goal is to travel to the bottom-right corner.
    /// You may only move down or right. 
    /// 
    /// In how many ways can you travel to the goal on a grid with dimensions m * n?
    /// Example. 2x3 -> 3 (RRD, RDR, DRR)
    /// Example. 1x1 -> 1
    /// Example. 0x1 -> 0
    /// </summary>
    public class Task2GridTraveller
    {
        private Dictionary<string, long> _memoization = new Dictionary<string, long>();

        /// <summary>
        /// Time and space O(n*m)
        /// </summary>
        public long GridTravellerTabulation(int m, int n)
        {
            var table = new long[m + 1, n + 1];
            table[1, 1] = 1;

            for (var i = 0; i <= m; i++)
            {
                for (var j = 0; j <= n; j++)
                {
                    var current = table[i, j];
                    if (i + 1 <= m)
                    {
                        table[i + 1, j] += current;
                    }
                    if (j + 1 <= n)
                    {
                        table[i, j + 1] += current;
                    }
                }
            }

            return table[m, n];
        }

        /// <summary>
        /// Order of arguments does not matter.
        /// 1. Create a unique key.
        /// 2. Check if the current key is ends in memo. Do not check for presence of child calls in the memo, this leads to complex code.
        /// O(m * n) time, O(m + n) space
        /// </summary>
        public long GridTravellerMemoization(int m, int n)
        {
            var key1 = $"{m},{n}";
            var key2 = $"{n},{m}";

            if (_memoization.ContainsKey(key1))
            {
                return _memoization[key1];
            }

            if (_memoization.ContainsKey(key2))
            {
                return _memoization[key2];
            }

            if (m == 1 && n == 1)
                return 1;

            if (m == 0 || n == 0)
                return 0;

            var result = GridTravellerMemoization(m - 1, n) + GridTravellerMemoization(m, n - 1);
            
            _memoization[key1] = result;
            _memoization[key2] = result;

            return result;
        }

        /// <summary>
        /// For a grid 3x3, if the first move is down, then the problem is the same as 2x3.
        /// 1. Identify overlapping problems.
        /// 2. Visualize using a tree.
        /// O(2^(n+m)) time, O(n+m) space
        /// </summary>
        public int GridTravellerNaive(int m, int n)
        {
            if (m == 1 && n == 1)
                return 1;

            if (m == 0 || n == 0)
                return 0;

            return GridTravellerNaive(m - 1, n) + GridTravellerNaive(m, n - 1);
        }

        public static void TestCase()
        {
            var traveller = new Task2GridTraveller();
            var shouldBe6 = traveller.GridTravellerMemoization(3, 3);
            var shouldBe2333606220 = traveller.GridTravellerTabulation(18, 18);
        }
    }
}
