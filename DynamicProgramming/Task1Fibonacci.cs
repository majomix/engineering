using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicProgramming
{
    /// <summary>
    /// Implement Fibonacci sequence generator.
    /// </summary>
    public class Task1Fibonacci
    {
        private Dictionary<int, UInt64> _memoization = new Dictionary<int, UInt64>();

        /// <summary>
        /// Seed initial values and re-use pre-calculated values
        /// space and time is O(n)
        /// </summary>
        public UInt64 GenerateFibonacciTabulation(int n)
        {
            var result = new UInt64[n + 3];

            result[1] = 1;

            for (var i = 0; i <= n; i++)
            {
                result[i + 1] += result[i];
                result[i + 2] += result[i];
            }

            return result[n];
        }

        public UInt64 GenerateFibonacciTabulationNaive(int levels)
        {
            var result = new List<UInt64>();

            result.Add(0);
            result.Add(1);

            for (var i = 1; i < levels; i++)
            {
                result.Add(result[i] + result[i - 1]);
            }

            return result.Last();
        }

        /// levels 50 - quadrilion steps
        /// time complexity is equal to number of recursive function calls (the number of nodes in a recursive tree)
        public UInt64 GenerateFibonacciRecursiveNaive(int levels)
        {
            if (levels <= 2)
            {
                return 1;
            }

            return GenerateFibonacciRecursiveNaive(levels - 1) + GenerateFibonacciRecursiveNaive(levels - 2);
        }

        public UInt64 GenerateFibonacciRecursiveMemoization(int levels)
        {
            if (_memoization.ContainsKey(levels))
            {
                return _memoization[levels];
            }

            if (levels <= 2)
            {
                return 1;
            }

            _memoization[levels] = GenerateFibonacciRecursiveMemoization(levels - 1) + GenerateFibonacciRecursiveMemoization(levels - 2);

            return _memoization[levels];
        }

        public static void TestCase()
        {
            var fibonacci = new Task1Fibonacci();
            var fibo = fibonacci.GenerateFibonacciTabulation(100);
            var fibo2 = fibonacci.GenerateFibonacciRecursiveMemoization(100);
        }
    }
}
