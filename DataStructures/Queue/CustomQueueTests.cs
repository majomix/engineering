using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Queue
{
    [TestFixture]
    internal class CustomQueueTests
    {
        public enum QueueType
        {
            DynamicArray,
            CircularDoublyLinkedList
        }

        [Test]
        [TestCase(QueueType.CircularDoublyLinkedList)]
        public void Stack_EnqueueDequeuePeekCount(QueueType type)
        {
            // arrange
            var queue = CreateQueue(type);

            // act
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Enqueue(7);
            queue.Enqueue(8);
            queue.Enqueue(9);

            // assert
            queue.Count.Should().Be(9);
            queue.Dequeue().Should().Be(1);
            queue.Dequeue().Should().Be(2);
            queue.Dequeue().Should().Be(3);
            queue.Dequeue().Should().Be(4);
            queue.Dequeue().Should().Be(5);
            queue.Dequeue().Should().Be(6);
            queue.Peek().Should().Be(7);
            queue.Peek().Should().Be(7);
            queue.Dequeue().Should().Be(7);
            queue.Dequeue().Should().Be(8);
            queue.Dequeue().Should().Be(9);

            queue.Count.Should().Be(0);
            queue.Peek().Should().Be(default);
            queue.Dequeue().Should().Be(default);
        }

        [Test]
        [TestCase(QueueType.CircularDoublyLinkedList)]
        public void Stack_Enqueue_Clear_Count(QueueType type)
        {
            // arrange
            var stack = CreateQueue(type);
            stack.Enqueue(1);
            stack.Enqueue(2);

            // act
            stack.Clear();

            // assert
            stack.Count.Should().Be(0);
        }

        [Test]
        [TestCase(QueueType.CircularDoublyLinkedList)]
        public void Stack_RepeatedEnqueueDequeue_Count(QueueType type)
        {
            // arrange
            var stack = CreateQueue(type);
            stack.Enqueue(1);
            stack.Dequeue();
            stack.Enqueue(2);
            stack.Dequeue();
            stack.Enqueue(3);
            stack.Enqueue(4);

            // act
            var finalDequeue = stack.Dequeue();

            // assert
            stack.Count.Should().Be(1);
            finalDequeue.Should().Be(3);
        }

        private ICustomQueue<int> CreateQueue(QueueType type)
        {
            switch (type)
            {
                //case QueueType.DynamicArray:
                //    return new CustomStackByDynamicArray<int>();
                case QueueType.CircularDoublyLinkedList:
                    return new CustomQueueByLinkedList<int>();
                default:
                    throw new ArgumentException("unexpected type");
            }
        }
    }
}