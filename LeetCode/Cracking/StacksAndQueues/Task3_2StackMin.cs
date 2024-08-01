using FluentAssertions;
using NUnit.Framework;

namespace LeetCode.Cracking.StacksAndQueues
{
    /// <summary>
    /// Design a stack which, in addition to push and pop, has a function min which returns the minimum element.
    /// Push, pop and min should all operate in O(1) time.
    ///
    /// Solutions:
    /// * keep a minimum element for each inserted element
    /// * keep additional stack tracking the minimums - possibly more space efficient
    /// </summary>
    internal record ValueAndMinimum(int Value, int Minimum);

    internal class MinStack : Stack<ValueAndMinimum>
    {
        public void Push(int value)
        {
            var minimum = value;

            if (Count != 0 && value >= Min())
            {
                minimum = Min();
            }

            var valueAndMinimum = new ValueAndMinimum(value, minimum);

            Push(valueAndMinimum);
        }

        public new int Pop()
        {
            return base.Pop().Value;
        }

        public int Min()
        {
            return Peek().Minimum;
        }
    }

    internal class MinStackWithLessSpace : Stack<int>
    {
        private readonly Stack<int> _minimumHistogram = new();

        public new void Push(int value)
        {
            if (_minimumHistogram.Count == 0)
            {
                _minimumHistogram.Push(value);
            }
            else
            {
                var currentMinimum = _minimumHistogram.Peek();
                if (value <= currentMinimum)
                {
                    _minimumHistogram.Push(value);
                }
            }

            base.Push(value);
        }

        public new int Pop()
        {
            var value = base.Pop();

            if (value == Min())
            {
                _minimumHistogram.Pop();
            }

            return value;
        }

        public int Min()
        {
            return _minimumHistogram.Peek();
        }
    }

    [TestFixture]
    internal class Task3_2StackMinTests
    {
        [Test]
        public void MinStack_PushPopPeekCount()
        {
            // arrange
            var stack = new MinStack();

            // act
            stack.Push(9);
            stack.Push(8);
            stack.Push(7);
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            stack.Push(3);
            stack.Push(2);
            stack.Push(1);

            // assert
            stack.Pop().Should().Be(1);
            stack.Pop().Should().Be(2);
            stack.Min().Should().Be(3);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(4);
            stack.Pop().Should().Be(5);
            stack.Min().Should().Be(6);
            stack.Pop().Should().Be(6);
            stack.Pop().Should().Be(7);
            stack.Pop().Should().Be(8);
            stack.Min().Should().Be(9);
            stack.Pop().Should().Be(9);
        }

        [Test]
        public void MinStackWithLessSpace_PushPopPeekCount()
        {
            // arrange
            var stack = new MinStackWithLessSpace();

            // act
            stack.Push(9);
            stack.Push(8);
            stack.Push(7);
            stack.Push(6);
            stack.Push(5);
            stack.Push(4);
            stack.Push(3);
            stack.Push(2);
            stack.Push(1);

            // assert
            stack.Pop().Should().Be(1);
            stack.Pop().Should().Be(2);
            stack.Min().Should().Be(3);
            stack.Pop().Should().Be(3);
            stack.Pop().Should().Be(4);
            stack.Pop().Should().Be(5);
            stack.Min().Should().Be(6);
            stack.Pop().Should().Be(6);
            stack.Pop().Should().Be(7);
            stack.Pop().Should().Be(8);
            stack.Min().Should().Be(9);
            stack.Pop().Should().Be(9);
        }
    }
}
