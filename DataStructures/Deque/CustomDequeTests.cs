using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Deque
{
    [TestFixture]
    internal class CustomDequeTests
    {
        [Test]
        public void Deque_PushBack_PopBack()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
        public void Deque_PushBack_PopFront()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
        public void Deque_PushFront_PopBack()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
        public void Deque_PushFront_PopFront()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
        public void Deque_PushFrontBack_PopFront()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
        public void Deque_PushFrontBack_PopBack()
        {
            // arrange
            var deque = new CustomDequeByLinkedList<int>();

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
    }
}
