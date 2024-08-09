using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.PriorityQueue
{
    [TestFixture]
    internal class CustomMinPriorityQueueTests
    {
        public enum PriorityQueueType
        {
            MinHeap,
            LinkedList,
            BinarySearchTree
        }

        [Test]
        [TestCase(PriorityQueueType.MinHeap)]
        [TestCase(PriorityQueueType.LinkedList)]
        [TestCase(PriorityQueueType.BinarySearchTree)]
        public void MinPriorityQueue_Insert_ExtractMin(PriorityQueueType priorityQueueType)
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
            priorityQueue.GetMinimum().Should().Be(1);
            priorityQueue.ExtractMinimum().Should().Be(1);
            priorityQueue.ExtractMinimum().Should().Be(2);
            priorityQueue.ExtractMinimum().Should().Be(3);
            priorityQueue.ExtractMinimum().Should().Be(4);
            priorityQueue.ExtractMinimum().Should().Be(5);
            priorityQueue.ExtractMinimum().Should().Be(6);
            priorityQueue.ExtractMinimum().Should().Be(7);
            priorityQueue.ExtractMinimum().Should().Be(8);
            priorityQueue.ExtractMinimum().Should().Be(9);
            var extractMinEmpty = () => priorityQueue.ExtractMinimum();
            extractMinEmpty.Should().Throw<InvalidOperationException>();
        }

        private IMinPriorityQueue<int, int> CreatePriorityQueue(PriorityQueueType type)
        {
            switch (type)
            {
                case PriorityQueueType.MinHeap:
                    return new CustomMinPriorityQueueByMinHeap<int, int>();
                case PriorityQueueType.LinkedList:
                    return new CustomMinPriorityQueueByLinkedList<int, int>();
                case PriorityQueueType.BinarySearchTree:
                    return new CustomMinPriorityQueueByBinarySearchTree<int, int>();
                default:
                    throw new ArgumentException("unexpected type");
            }
        }
    }
}
