using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Heap
{
    [TestFixture]
    internal class CustomMinHeapByDynamicArrayTests
    {
        [Test]
        public void MinHeap_Insert_ExtractMin()
        {
            // arrange
            var heap = new CustomMinHeapByDynamicArray<int, int>();

            // act
            heap.Insert(4, 4);
            heap.Insert(2, 2);
            heap.Insert(1, 1);
            heap.Insert(3, 3);
            heap.Insert(8, 8);
            heap.Insert(9, 9);
            heap.Insert(7, 7);
            heap.Insert(6, 6);
            heap.Insert(5, 5);

            // assert
            heap.PeekMin().Should().Be(1);
            heap.ExtractMin().Should().Be(1);
            heap.ExtractMin().Should().Be(2);
            heap.ExtractMin().Should().Be(3);
            heap.ExtractMin().Should().Be(4);
            heap.ExtractMin().Should().Be(5);
            heap.ExtractMin().Should().Be(6);
            heap.ExtractMin().Should().Be(7);
            heap.ExtractMin().Should().Be(8);
            heap.ExtractMin().Should().Be(9);
            var extractMinEmpty = () => heap.ExtractMin();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void MinHeap_Insert_Clear()
        {
            // arrange
            var heap = new CustomMinHeapByDynamicArray<int, int>();

            // act
            heap.Insert(4, 4);
            heap.Insert(2, 2);
            heap.Insert(1, 1);
            heap.Clear();

            // assert
            var extractMinEmpty = () => heap.ExtractMin();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void MinHeap_BuildHeap()
        {
            // arrange
            var list = new[] { 7, 9, 8, 4, 3, 1, 2, 5, 6 };
            var heap = new CustomMinHeapByDynamicArray<int, int>();

            // act
            heap.BuildHeap(list, value => value);

            // assert
            heap.PeekMin().Should().Be(1);
            heap.ExtractMin().Should().Be(1);
            heap.ExtractMin().Should().Be(2);
            heap.ExtractMin().Should().Be(3);
            heap.ExtractMin().Should().Be(4);
            heap.ExtractMin().Should().Be(5);
            heap.ExtractMin().Should().Be(6);
            heap.ExtractMin().Should().Be(7);
            heap.ExtractMin().Should().Be(8);
            heap.ExtractMin().Should().Be(9);
            var extractMinEmpty = () => heap.ExtractMin();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }
    }
}
