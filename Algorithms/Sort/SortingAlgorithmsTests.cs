using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Sort
{
    [TestFixture]
    internal class SortingAlgorithmsTests
    {
        internal enum SortingAlgorithm
        {
            InsertionSort,
            SelectionSort,
            BubbleSort,
            ShellSort,
            QuickSort,
            MergeSort,
            HeapSort,
            CountingSort,
            RadixSort
        }

        private static object[] testCases =
        {
            new object[] { SortingAlgorithm.SelectionSort },
            new object[] { SortingAlgorithm.InsertionSort },
            new object[] { SortingAlgorithm.BubbleSort },
            new object[] { SortingAlgorithm.ShellSort },
            new object[] { SortingAlgorithm.QuickSort },
            new object[] { SortingAlgorithm.MergeSort },
            new object[] { SortingAlgorithm.HeapSort },
            new object[] { SortingAlgorithm.CountingSort },
        };

        [TestCaseSource(nameof(testCases))]
        public void SortShuffledDataTest(SortingAlgorithm algorithm)
        {
            // arrange
            var sut = CreateSut(algorithm);
            var input = new[] { 9, 7, 5, 6, 4, 2, 3, 14, 0, -1 };

            // act
            var result = sut.Sort(input, i => i);

            // assert
            result.Should().BeInAscendingOrder();
        }

        [TestCaseSource(nameof(testCases))]
        public void SortReversedSortedDataTest(SortingAlgorithm algorithm)
        {
            // arrange
            var sut = CreateSut(algorithm);
            var input = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, -1, -2, -3 };

            // act
            var result = sut.Sort(input, i => i);

            // assert
            result.Should().BeInAscendingOrder();
        }

        [TestCaseSource(nameof(testCases))]
        public void SortPresortedDataTest(SortingAlgorithm algorithm)
        {
            // arrange
            var sut = CreateSut(algorithm);
            var input = new[] { -3, -2, -1, 0, 1, 2, 3, 4, 5, 6 };

            // act
            var result = sut.Sort(input, i => i);

            // assert
            result.Should().BeInAscendingOrder();
        }

        [TestCase(SortingAlgorithm.RadixSort)]
        public void RadixSortTest(SortingAlgorithm algorithm)
        {
            // arrange
            var sut = CreateSut(algorithm);
            var input = new[] { 329, 457, 657, 59, 53, 4, 8, 1789, 1256 };

            // act
            var result = sut.Sort(input, i => i);

            // assert
            result.Should().BeInAscendingOrder();
        }

        private ISortingAlgorithm<int, int> CreateSut(SortingAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case SortingAlgorithm.BubbleSort:
                    return new BubbleSortAlgorithm<int, int>();
                case SortingAlgorithm.InsertionSort:
                    return new InsertionSortAlgorithm<int, int>();
                case SortingAlgorithm.SelectionSort:
                    return new SelectionSortAlgorithm<int, int>();
                case SortingAlgorithm.ShellSort:
                    return new ShellSortAlgorithm<int, int>();
                case SortingAlgorithm.QuickSort:
                    return new QuickSortAlgorithm<int, int>();
                case SortingAlgorithm.MergeSort:
                    return new MergeSortAlgorithm<int, int>();
                case SortingAlgorithm.HeapSort:
                    return new HeapSortAlgorithm<int, int>();
                case SortingAlgorithm.CountingSort:
                    return new CountingSortAlgorithm<int>();
                case SortingAlgorithm.RadixSort:
                    return new RadixSortAlgorithm<int>();
                default:
                    throw new NotImplementedException("Algorithm not implemented.");
            }
        }
    }
}
