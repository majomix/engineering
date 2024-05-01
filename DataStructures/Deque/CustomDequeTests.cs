using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Deque
{
    [TestFixture]
    internal class CustomDequeTests
    {
        public enum DequeType
        {
            CircularArray,
            CircularDoublyLinkedList
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushBack_PopBack(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);

            // assert
            deque.Count.Should().Be(3);
            deque.PopBack().Should().Be(3);
            deque.PopBack().Should().Be(2);
            deque.PopBack().Should().Be(1);
            deque.Count.Should().Be(0);
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushBack_PopFront(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);

            // assert
            deque.Count.Should().Be(3);
            deque.PopFront().Should().Be(1);
            deque.PopFront().Should().Be(2);
            deque.PopFront().Should().Be(3);
            deque.Count.Should().Be(0);
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushFront_PopBack(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);

            // assert
            deque.Count.Should().Be(3);
            deque.PopBack().Should().Be(1);
            deque.PopBack().Should().Be(2);
            deque.PopBack().Should().Be(3);
            deque.Count.Should().Be(0);
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushFront_PopFront(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);

            // assert
            deque.Count.Should().Be(3);
            deque.PopFront().Should().Be(3);
            deque.PopFront().Should().Be(2);
            deque.PopFront().Should().Be(1);
            deque.Count.Should().Be(0);
        }


        [Test]
        public void Deque_CircularArray_ReallocationNoWrap()
        {
            // arrange
            var deque = new CustomDequeByCircularArray<int>();
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);
            deque.PushBack(4);

            // act
            deque.PushBack(5);

            // assert
            deque.Count.Should().Be(5);
            deque.PopBack().Should().Be(5);
            deque.PopBack().Should().Be(4);
            deque.PopBack().Should().Be(3);
            deque.PopBack().Should().Be(2);
            deque.PopBack().Should().Be(1);
            deque.Count.Should().Be(0);
        }

        [Test]
        public void Deque_CircularArray_ReallocationWrap()
        {
            // arrange
            var deque = new CustomDequeByCircularArray<int>();
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);
            deque.PushBack(4);
            deque.PopFront();
            deque.PopFront();
            deque.PushBack(5);
            deque.PushBack(6);

            // act
            deque.PushBack(7);

            // assert
            deque.Count.Should().Be(5);
            deque.PopBack().Should().Be(7);
            deque.PopBack().Should().Be(6);
            deque.PopBack().Should().Be(5);
            deque.PopBack().Should().Be(4);
            deque.PopBack().Should().Be(3);
            deque.Count.Should().Be(0);
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushFrontBack_PopFront(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushFront(1);
            deque.PushBack(2);
            deque.PushFront(3);
            deque.PushBack(4);
            deque.PushFront(5);
            deque.PushFront(6);
            deque.PushBack(7);
            deque.PushBack(8);

            // assert
            deque.Count.Should().Be(8);
            deque.PopFront().Should().Be(6);
            deque.PopFront().Should().Be(5);
            deque.PopFront().Should().Be(3);
            deque.PopFront().Should().Be(1);
            deque.PopFront().Should().Be(2);
            deque.PopFront().Should().Be(4);
            deque.PopFront().Should().Be(7);
            deque.PopFront().Should().Be(8);
            deque.Count.Should().Be(0);
        }

        [Test]
        [TestCase(DequeType.CircularArray)]
        [TestCase(DequeType.CircularDoublyLinkedList)]
        public void Deque_PushFrontBack_PopBack(DequeType type)
        {
            // arrange
            var deque = CreateDeque(type);

            // act
            deque.PushFront(1);
            deque.PushBack(2);
            deque.PushFront(3);
            deque.PushBack(4);
            deque.PushFront(5);
            deque.PushFront(6);
            deque.PushBack(7);
            deque.PushBack(8);

            // assert
            deque.Count.Should().Be(8);
            deque.PopBack().Should().Be(8);
            deque.PopBack().Should().Be(7);
            deque.PopBack().Should().Be(4);
            deque.PopBack().Should().Be(2);
            deque.PopBack().Should().Be(1);
            deque.PopBack().Should().Be(3);
            deque.PopBack().Should().Be(5);
            deque.PopBack().Should().Be(6);
            deque.Count.Should().Be(0);
        }

        private ICustomDeque<int> CreateDeque(DequeType type)
        {
            switch (type)
            {
                case DequeType.CircularArray:
                    return new CustomDequeByCircularArray<int>();
                case DequeType.CircularDoublyLinkedList:
                    return new CustomDequeByLinkedList<int>();
                default:
                    throw new ArgumentException("unexpected type");
            }
        }
    }
}
