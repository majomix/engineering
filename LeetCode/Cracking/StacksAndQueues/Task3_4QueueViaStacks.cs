using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// Create a class which implements a queue using two stacks.
    ///
    /// Discussion:
    /// Stack is LIFO (last in, first out) and Queue is FIFO (first in, first out). Difference is order in which items are taken.
    /// To mimic queue by stack, order has to be flipped through second stack.
    /// 
    /// Solutions:
    /// * flip order by always copying all items to second stack at dequeue and then back
    /// 1. S1: T 1 2 3 4; S2: T
    /// 2. S1: T; S2: T 4 3 2 1
    /// 3. Pop S2 (4)
    /// 4. S1: T 1 2 3 4
    /// 
    /// * copy items one way only when second stack is empty - less copy operations
    /// 1. S1: T 1 2 3 4; S2: T
    /// 2. S1: T; S2: T 4 3 2 1
    /// 3. Pop S2 (4)
    /// 4. Push S1 (8, 7, 6, 5)
    /// 5. S1: T 5 6 7 8; S2: 3 2 1
    /// 6: S1: T; S2: 8 7 6 5 3 2 1
    /// 7. Pop S2 (8)
    /// </summary>
    internal class QueueViaStacks
    {
        private readonly Stack<int> _leftStack = new();
        private readonly Stack<int> _rightStack = new();


        public void Enqueue(int value)
        {
            _leftStack.Push(value);
        }

        public int Dequeue()
        {
            var itemsToPop = _leftStack.Count;
            for (var i = 0; i < itemsToPop; i++)
            {
                var valueToCopy = _leftStack.Pop();
                _rightStack.Push(valueToCopy);
            }

            var value = _rightStack.Pop();

            itemsToPop = _rightStack.Count;
            for (var i = 0; i < itemsToPop; i++)
            {
                var valueToCopy = _rightStack.Pop();
                _leftStack.Push(valueToCopy);
            }

            return value;
        }
    }

    internal class QueueViaStacksMoreOptimal
    {
        private readonly Stack<int> _leftStack = new();
        private readonly Stack<int> _rightStack = new();


        public void Enqueue(int value)
        {
            _leftStack.Push(value);
        }

        public int Dequeue()
        {
            if (_rightStack.Count != 0)
                return _rightStack.Pop();

            var itemsToPop = _leftStack.Count;
            for (var i = 0; i < itemsToPop; i++)
            {
                var valueToCopy = _leftStack.Pop();
                _rightStack.Push(valueToCopy);
            }

            return _rightStack.Pop();
        }
    }

    [TestFixture]
    internal class Task3_4QueueViaStacksTests
    {
        [Test]
        public void QueueViaStacks_EnqueueDequeue()
        {
            // arrange
            var queue = new QueueViaStacks();

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
            queue.Dequeue().Should().Be(1);
            queue.Dequeue().Should().Be(2);
            queue.Dequeue().Should().Be(3);
            queue.Dequeue().Should().Be(4);
            queue.Dequeue().Should().Be(5);
            queue.Dequeue().Should().Be(6);
            queue.Dequeue().Should().Be(7);
            queue.Dequeue().Should().Be(8);
            queue.Dequeue().Should().Be(9);
        }

        [Test]
        public void QueueViaStacksMoreOptimal_EnqueueDequeue()
        {
            // arrange
            var queue = new QueueViaStacksMoreOptimal();

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
            queue.Dequeue().Should().Be(1);
            queue.Dequeue().Should().Be(2);
            queue.Dequeue().Should().Be(3);
            queue.Dequeue().Should().Be(4);
            queue.Dequeue().Should().Be(5);
            queue.Dequeue().Should().Be(6);
            queue.Dequeue().Should().Be(7);
            queue.Dequeue().Should().Be(8);
            queue.Dequeue().Should().Be(9);
        }
    }
}
