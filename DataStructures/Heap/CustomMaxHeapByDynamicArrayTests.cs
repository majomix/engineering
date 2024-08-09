using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Heap
{
    [TestFixture]
    internal class CustomMaxHeapByDynamicArrayTests
    {
        [Test]
        public void MaxHeap_Insert_ExtractMax()
        {
            // arrange
            var heap = new CustomMaxHeapByDynamicArray<int, int>();

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
            heap.PeekMax().Should().Be(9);
            heap.ExtractMax().Should().Be(9);
            heap.ExtractMax().Should().Be(8);
            heap.ExtractMax().Should().Be(7);
            heap.ExtractMax().Should().Be(6);
            heap.ExtractMax().Should().Be(5);
            heap.ExtractMax().Should().Be(4);
            heap.ExtractMax().Should().Be(3);
            heap.ExtractMax().Should().Be(2);
            heap.ExtractMax().Should().Be(1);
            var extractMinEmpty = () => heap.ExtractMax();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void MaxHeap_Insert_Clear()
        {
            // arrange
            var heap = new CustomMaxHeapByDynamicArray<int, int>();

            // act
            heap.Insert(4, 4);
            heap.Insert(2, 2);
            heap.Insert(1, 1);
            heap.Clear();

            // assert
            var extractMinEmpty = () => heap.ExtractMax();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void MaxHeap_BuildHeap()
        {
            // arrange
            var list = new[] { 7, 9, 8, 4, 3, 1, 2, 5, 6 };
            var heap = new CustomMaxHeapByDynamicArray<int, int>();

            // act
            heap.BuildHeap(list, value => value);

            // assert
            heap.PeekMax().Should().Be(9);
            heap.ExtractMax().Should().Be(9);
            heap.ExtractMax().Should().Be(8);
            heap.ExtractMax().Should().Be(7);
            heap.ExtractMax().Should().Be(6);
            heap.ExtractMax().Should().Be(5);
            heap.ExtractMax().Should().Be(4);
            heap.ExtractMax().Should().Be(3);
            heap.ExtractMax().Should().Be(2);
            heap.ExtractMax().Should().Be(1);
            var extractMinEmpty = () => heap.ExtractMax();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }
    }
}
