using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.PriorityQueue
{
    [TestFixture]
    internal class CustomMaxPriorityQueueTests
    {
        public enum PriorityQueueType
        {
            MaxHeap,
            LinkedList,
            BinarySearchTree
        }

        [Test]
        [TestCase(PriorityQueueType.MaxHeap)]
        [TestCase(PriorityQueueType.LinkedList)]
        [TestCase(PriorityQueueType.BinarySearchTree)]
        public void MaxPriorityQueue_Insert_ExtractMax(PriorityQueueType priorityQueueType)
        {
            // arrange
            var priorityQueue = CreatePriorityQueue(priorityQueueType);

            // act
            priorityQueue.Insert(4, 4);
            priorityQueue.Insert(2, 2);
            priorityQueue.Insert(1, 1);
            priorityQueue.Insert(3, 3);
            priorityQueue.Insert(8, 8);
            priorityQueue.Insert(9, 9);
            priorityQueue.Insert(7, 7);
            priorityQueue.Insert(6, 6);
            priorityQueue.Insert(5, 5);

            // assert
            priorityQueue.PeekMaximum().Should().Be(9);
            priorityQueue.ExtractMaximum().Should().Be(9);
            priorityQueue.ExtractMaximum().Should().Be(8);
            priorityQueue.ExtractMaximum().Should().Be(7);
            priorityQueue.ExtractMaximum().Should().Be(6);
            priorityQueue.ExtractMaximum().Should().Be(5);
            priorityQueue.ExtractMaximum().Should().Be(4);
            priorityQueue.ExtractMaximum().Should().Be(3);
            priorityQueue.ExtractMaximum().Should().Be(2);
            priorityQueue.ExtractMaximum().Should().Be(1);
            var extractMinEmpty = () => priorityQueue.ExtractMaximum();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        private IMaxPriorityQueue<int, int> CreatePriorityQueue(PriorityQueueType type)
        {
            switch (type)
            {
                case PriorityQueueType.MaxHeap:
                    return new CustomMaxPriorityQueueByMaxHeap<int, int>();
                case PriorityQueueType.LinkedList:
                    return new CustomMaxPriorityQueueByLinkedList<int, int>();
                case PriorityQueueType.BinarySearchTree:
                    return new CustomMaxPriorityQueueByBinarySearchTree<int, int>();
                default:
                    throw new ArgumentException("unexpected type");
            }
        }
    }
}
