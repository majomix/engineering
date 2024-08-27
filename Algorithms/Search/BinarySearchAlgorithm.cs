using DataStructures.DynamicArray;
using NUnit.Framework;
using FluentAssertions;

namespace Algorithms.Search
{
    internal class BinarySearchAlgorithm
    {
        /// <summary>
        /// Purpose:
        /// Find the index of a key within sorted array.
        ///
        /// Complexity:
        /// Time: O(log n)
        /// Space: O(1)
        /// </summary>
        /// <param name="array">Array which must be sorted.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>Index of searched item or 1's complement of the index of the nearest highest value if exists.</returns>
        public int BinarySearch<T>(CustomDynamicArray<T> array, T key)
        {
            if (array.Count == 0)
                return ~1;

            var comparer = Comparer<T>.Default;

            var left = 0u;
            var right = array.Count - 1;

            while (left <= right)
            {
                // Improvement! No risk of overflow.
                // var middle = (left + right) / 2
                var middle = left + (right - left) / 2;
                
                var comparisonResult = comparer.Compare(array[middle], key);

                switch (comparisonResult)
                {
                    case 0:
                        return (int)middle;
                    case 1:
                        right = middle - 1;
                        break;
                    case -1:
                        left = middle + 1;
                        break;
                }
            }

            return ~((int)left);
        }

        /// <summary>
        /// Purpose:
        /// Find the index of a key within sorted array.
        ///
        /// Complexity:
        /// Time: O(log n)
        /// Space: O(1)
        /// </summary>
        /// <param name="array">Array which must be sorted.</param>
        /// <param name="key">Key to search for.</param>
        /// <returns>Index of searched item or 1's complement of the index of the nearest highest value if exists.</returns>
        public int BinarySearchRecursively<T>(CustomDynamicArray<T> array, T key)
        {
            if (array.Count == 0)
                return ~1;

            return BinarySearchRecursivelyInternal(array, key, 0, array.Count - 1);
        }

        private int BinarySearchRecursivelyInternal<T>(CustomDynamicArray<T> array, T key, uint left, uint right)
        {
            if (left > right)
            {
                return ~((int)left);
            }

            var middle = left + (right - left) / 2;

            var comparer = Comparer<T>.Default;
            var comparisonResult = comparer.Compare(array[middle], key);

            switch (comparisonResult)
            {
                case 0:
                    return (int)middle;
                case 1:
                    return BinarySearchRecursivelyInternal(array, key, left, middle - 1);
                case -1:
                    return BinarySearchRecursivelyInternal(array, key, middle + 1, right);
                default:
                    throw new Exception();
            }
        }
    }

    [TestFixture]
    internal class BinarySearchTests
    {
        private static object[] testCases =
        {
            new object[] { new[] { 1 }, 1, 0 },
            new object[] { new[] { 1, 2 }, 2, 1 },
            new object[] { new[] { 1, 2, 3 }, 2, 1 },
            new object[] { new[] { 1, 2, 3 }, 3, 2 },
            new object[] { new[] { -3, -2, -1 }, -1, 2 },
            new object[] { new[] { 1, 2, 3, 4, 5, 6, 7 }, 4, 3 },
            new object[] { new[] { 1, 2, 3, 4, 5, 6, 7 }, 2, 1 },
            new object[] { new[] { 1, 2, 3, 4, 5, 6, 7 }, 6, 5 },
            new object[] { new[] { 1, 2 }, 3, ~2 },
            new object[] { new[] { 1, 3 }, 2, ~1 },
            new object[] { new[] { 1 }, 2, ~1 },
            new object[] { Array.Empty<int>(), 0, ~1 },
        };

        [TestCaseSource(nameof(testCases))]
        public void BinarySearchTest(int[] items, int key, int expectedResult)
        {
            // arrange
            var array = new CustomDynamicArray<int>(items);
            var binarySearch = new BinarySearchAlgorithm();

            // act
            var result = binarySearch.BinarySearch(array, key);

            // assert
            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(testCases))]
        public void BinarySearchRecursivelyTest(int[] items, int key, int expectedResult)
        {
            // arrange
            var array = new CustomDynamicArray<int>(items);
            var binarySearch = new BinarySearchAlgorithm();

            // act
            var result = binarySearch.BinarySearchRecursively(array, key);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}
